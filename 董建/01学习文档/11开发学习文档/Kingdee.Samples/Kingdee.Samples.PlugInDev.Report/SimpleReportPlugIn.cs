﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Kingdee.BOS;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core.Report;
using Kingdee.BOS.Contracts.Report;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.Orm.DataEntity;

namespace Kingdee.Samples.PlugInDev.Report
{
    [Description("简单账表演示,包含过滤页面条件")]
    [HotUpdate]
    public class SimpleReportPlugIn : SysReportBaseService
    {
        public override void Initialize()
        {
            base.Initialize();
            // 简单账表类型：普通、树形、分页
            this.ReportProperty.ReportType = ReportType.REPORTTYPE_NORMAL;

            // 
            this.IsCreateTempTableByPlugin = true;

            // 标识报表的列必须通过UI设计，不考虑动态列
            this.ReportProperty.IsUIDesignerColumns = false;

            // 标识报表是否支持分组汇总
            this.ReportProperty.IsGroupSummary = true;

            // 是否锁定表格列
            this.ReportProperty.SimpleAllCols = false;

            // 单据主键：两行FID相同，则为同一单的两条分录，单据编号可以不重复显示
            this.ReportProperty.PrimaryKeyFieldName = "FID";

            // 默认只显示合计和明细数据，不显示小计数据
            this.ReportProperty.IsDefaultOnlyDspSumAndDetailData = true;

            // 报表主键字段名：默认为FIDENTITYID，可以修改
            //this.ReportProperty.IdentityFieldName = "FIDENTITYID";
            //
            // 设置精度控制
            List<DecimalControlField> list = new List<DecimalControlField>();
            // 数量
            list.Add(new DecimalControlField
            {
                ByDecimalControlFieldName = "FQty",
                DecimalControlFieldName = "FUnitPrecision"
            });
            // 单价
            list.Add(new DecimalControlField
            {
                ByDecimalControlFieldName = "FTAXPRICE",
                DecimalControlFieldName = "FPRICEDIGITS"
            });
            // 金额
            list.Add(new DecimalControlField
            {
                ByDecimalControlFieldName = "FALLAMOUNT",
                DecimalControlFieldName = "FAMOUNTDIGITS"
            });

            //精度控制字段集合
            this.ReportProperty.DecimalControlFieldList = list;
        }

        /// <summary>
        /// 向报表临时表，插入报表数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tableName"></param>
        public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        {
            base.BuilderReportSqlAndTempTable(filter, tableName);

            // 拼接过滤条件 ： filter
            // 略

            // 默认排序字段：需要从filter中取用户设置的排序字段

            //gen sql :  ROW_NUMBER() OVER(ORDER BY  t1.FMATERIALID asc ) FIDENTITYID 
            if (filter.FilterParameter.SortString.IsNullOrEmpty())
            {
                this.KSQL_SEQ = string.Format(this.KSQL_SEQ, " t1.FMATERIALID asc");
            }
            else
            {

                this.KSQL_SEQ = string.Format(this.KSQL_SEQ, filter.FilterParameter.SortString);
            }

            // 取数SQL
            // FID, FEntryId, 编号、状态、物料、数量、单位、单位精度、单价、价税合计
            string sql = string.Format(@"/*dialect*/
                                select t0.FID, t1.FENTRYID
                                       ,t0.FBILLNO
                                       ,t0.FDate
                                       ,t0.FDOCUMENTSTATUS
                                       ,t2.FLOCALCURRID
                                       ,ISNULL(t20.FPRICEDIGITS,4) AS FPRICEDIGITS
                                       ,ISNULL(t20.FAMOUNTDIGITS,2) AS FAMOUNTDIGITS
                                       ,t1.FMATERIALID
                                       ,t1M_L.FNAME as FMaterialName
                                       ,t1.FQTY
                                       ,t1u.FPRECISION as FUnitPrecision
                                       ,t1U_L.FNAME as FUnitName
                                       ,t1f.FTAXPRICE
                                       ,t1f.FALLAMOUNT
                                        ,t1f.FDISCOUNTRATE   --折扣率
                                        ,t1f.FTAXRATE        --税率
                                        ,{0}
                                  into {1}
                                  from T_PUR_POORDER t0
                                 inner join T_PUR_POORDERFIN t2 on (t0.FID = t2.FID)
                                  left join T_BD_CURRENCY t20 on (t2.FLOCALCURRID = t20.FCURRENCYID)
                                 inner join T_PUR_POORDERENTRY t1 on (t0.FID = t1.FID)
                                  left join T_BD_MATERIAL_L t1M_L on (t1.FMATERIALID = t1m_l.FMATERIALID and t1M_L.FLOCALEID = 2052)
                                 inner join T_PUR_POORDERENTRY_F t1F on (t1.FENTRYID = t1f.FENTRYID)
                                  left join T_BD_UNIT t1U on (t1f.FPRICEUNITID = t1u.FUNITID)
                                  left join T_BD_UNIT_L t1U_L on (t1U.FUNITID = t1U_L.FUNITID and t1U_L.FLOCALEID = 2052) 
                                where 1=1 ",
                        KSQL_SEQ,
                        tableName);


            //添加快捷过滤
            string quickStr = this.GetQuickFilter(filter);
            if (!quickStr.IsNullOrEmptyOrWhiteSpace())
            {
                sql += string.Format("AND {0}", quickStr);
            }

            //添加条件过滤
            string conditionStr = this.GetCondition(filter);
            if (!conditionStr.IsNullOrEmptyOrWhiteSpace())
            {
                sql += string.Format("AND {0}", conditionStr);
            }

            DBUtils.Execute(this.Context, sql);
        }

        /// <summary>
        /// 设置报表列
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override ReportHeader GetReportHeaders(IRptParams filter)
        {
            // FID, FEntryId, 编号、状态、物料、数量、单位、单位精度、单价、价税合计
            ReportHeader header = new ReportHeader();
            // 编号
            var dateHeader = header.AddChild("FDate", new LocaleValue("日期"));
            dateHeader.ColIndex = 0;
            var status = header.AddChild("FDocumentStatus", new LocaleValue("状态"));
            status.ColIndex = 1;
            var billNo = header.AddChild("FBillNo", new LocaleValue("单据编号"));
            billNo.ColIndex = 2;
            billNo.IsHyperlink = true;          // 支持超链接
            var material = header.AddChild("FMaterialName", new LocaleValue("物料"));
            material.ColIndex = 3;
            var qty = header.AddChild("FQty", new LocaleValue("数量"), SqlStorageType.SqlDecimal);
            qty.ColIndex = 4;
            var unit = header.AddChild("FUnitName", new LocaleValue("单位"));
            unit.ColIndex = 5;
            var price = header.AddChild("FTAXPRICE", new LocaleValue("含税价"), SqlStorageType.SqlDecimal);
            price.ColIndex = 6;
            var amount = header.AddChild("FALLAMOUNT", new LocaleValue("价税合计"), SqlStorageType.SqlDecimal);
            amount.ColIndex = 7;

            //add 2 columns
            var taxRate = header.AddChild("FTAXRATE", new LocaleValue("税率"), SqlStorageType.SqlDecimal);
            taxRate.ColIndex = 8;
            var discount = header.AddChild("FDISCOUNTRATE", new LocaleValue("折扣率"), SqlStorageType.SqlDecimal);
            discount.ColIndex = 9;
            return header;
        }

        /// <summary>
        /// 设置报表标题
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override ReportTitles GetReportTitles(IRptParams filter)
        {
            var result = base.GetReportTitles(filter);
            DynamicObject dyFilter = filter.FilterParameter.CustomFilter;
            if (dyFilter != null)
            {
                if (result == null)
                {
                    result = new ReportTitles();
                }
                result.AddTitle("F_PAEZ_StartDate", Convert.ToString(dyFilter["PAEZ_StartDate"]));
                result.AddTitle("F_PAEZ_EndDate", Convert.ToString(dyFilter["PAEZ_EndDate"]));               
            }
            return result;
        }

        /// <summary>
        /// 设置报表合计列
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override List<SummaryField> GetSummaryColumnInfo(IRptParams filter)
        {
            var result = base.GetSummaryColumnInfo(filter);
            result.Add(new SummaryField("FQty", Kingdee.BOS.Core.Enums.BOSEnums.Enu_SummaryType.SUM));
            result.Add(new SummaryField("FALLAMOUNT", Kingdee.BOS.Core.Enums.BOSEnums.Enu_SummaryType.SUM));
            return result;
        }

        /// <summary>
        /// 快捷过滤面板条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetQuickFilter(IRptParams filter)
        {
            string dateStartKey = "PAEZ_StartDate";
            string dateEndKey = "PAEZ_EndDate";

            string filterStr = string.Empty;

            if (filter.FilterParameter.CustomFilter.Contains(dateStartKey)
                && filter.FilterParameter.CustomFilter.Contains(dateEndKey))
            {
                var dateStartValue = filter.FilterParameter.CustomFilter[dateStartKey];
                var dateEndValue = filter.FilterParameter.CustomFilter[dateEndKey];

                if (!dateStartValue.IsNullOrEmptyOrWhiteSpace() && !dateEndValue.IsNullOrEmptyOrWhiteSpace())
                {
                    filterStr += string.Format(" FDate >= '{0}'", dateStartValue.ToString());
                    filterStr += string.Format(" AND FDate < '{0}'", Convert.ToDateTime(dateEndValue).AddDays(1));
                }
            }

            return filterStr;
        }

        /// <summary>
        /// 过滤条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetCondition(IRptParams filter)
        {
            string conditionStr = filter.FilterParameter.FilterString;
            if (!string.IsNullOrEmpty(conditionStr))
            {
                return conditionStr;
            }
            return string.Empty;
        }
    }
}
