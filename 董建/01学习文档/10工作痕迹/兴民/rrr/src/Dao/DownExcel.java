package Dao;

import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletResponse;

import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFCellStyle;
import org.apache.poi.hssf.usermodel.HSSFDataFormat;
import org.apache.poi.hssf.usermodel.HSSFFont;
import org.apache.poi.hssf.usermodel.HSSFRichTextString;
import org.apache.poi.hssf.usermodel.HSSFRow;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.hssf.util.HSSFColor;
import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.CreationHelper;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.ss.usermodel.Sheet;
import org.apache.poi.ss.usermodel.Workbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

public class DownExcel {
	
	conn conn = new conn();
	List<model> downExcel = conn.Show();
	public void testHSSF() throws Exception {
	    //  创建一个工作簿
	    HSSFWorkbook wb = new HSSFWorkbook();
	    //  创建一个工作表
	    HSSFSheet sheet = wb.createSheet();

	    //  创建字体
	    HSSFFont font1 = wb.createFont();
	    HSSFFont font2 = wb.createFont();
	    font1.setFontHeightInPoints((short) 14);
	    font1.setColor(HSSFColor.HSSFColorPredefined.RED.getIndex());
	    font2.setFontHeightInPoints((short) 12);
	    font2.setColor(HSSFColor.HSSFColorPredefined.BLUE.getIndex());
	    //  创建单元格样式
	    HSSFCellStyle css1 = wb.createCellStyle();
	    HSSFCellStyle css2 = wb.createCellStyle();
	    HSSFDataFormat df = wb.createDataFormat();
	    //  设置单元格字体及格式
	    css1.setFont(font1);
	    css1.setDataFormat(df.getFormat("#,##0.0"));
	    css2.setFont(font2);
	    css2.setDataFormat(HSSFDataFormat.getBuiltinFormat("text"));

	    //  创建行
	    int i=0;
	    while(i<downExcel.size()) {
	    	 HSSFRow row = sheet.createRow(i);
	    	 
	    	 HSSFCell cell = row.createCell(1);
	            cell.setCellValue(downExcel.get(i).getEname());
	            cell.setCellStyle(css1);
	            
	            HSSFCell cell2 = row.createCell(2);
	            cell2.setCellValue(downExcel.get(i).getEtype());
	            cell2.setCellStyle(css1);
	            
	            HSSFCell cell3 = row.createCell(3);
	            cell3.setCellValue(downExcel.get(i).getStatus());
	            cell3.setCellStyle(css1);
	            
	            HSSFCell cell4 = row.createCell(4);
	            cell.setCellValue(downExcel.get(i).getElocation());
	            cell.setCellStyle(css1);
	            
	            HSSFCell cell5 = row.createCell(5);
	            cell2.setCellValue(downExcel.get(i).getAttribute1());
	            cell2.setCellStyle(css1);
	            
	            HSSFCell cell6 = row.createCell(6);
	            cell3.setCellValue(downExcel.get(i).getIcontent());
	            cell3.setCellStyle(css1);
	            
	            HSSFCell cell7 = row.createCell(7);
	            cell.setCellValue(downExcel.get(i).getIresult());
	            cell.setCellStyle(css1);
	            
	            HSSFCell cell8 = row.createCell(8);
	            cell2.setCellValue(downExcel.get(i).getName());
	            cell2.setCellStyle(css1);
	            
	            HSSFCell cell9 = row.createCell(9);
	            cell3.setCellValue(downExcel.get(i).getCreation_date());
	            cell3.setCellStyle(css1);
	            
	            HSSFCell cell10 = row.createCell(10);
	            cell.setCellValue(downExcel.get(i).getName2());
	            cell.setCellStyle(css1);
	            
	            HSSFCell cell11 = row.createCell(11);
	            cell2.setCellValue(downExcel.get(i).getChulidate());
	            cell2.setCellStyle(css1);
	            
	            HSSFCell cell12 = row.createCell(12);
	            cell3.setCellValue(downExcel.get(i).getChulicuoshi());
	            cell3.setCellStyle(css1);
	            
	            HSSFCell cell13 = row.createCell(13);
	            cell3.setCellValue(downExcel.get(i).getImageurl());
	            cell3.setCellStyle(css1);
	            
	    	i++;
	    	
	    }
	  

	    //  写文件
	    FileOutputStream fos = new FileOutputStream("D:/wb.xls");
	    wb.write(fos);
	    fos.close();
	}



}
