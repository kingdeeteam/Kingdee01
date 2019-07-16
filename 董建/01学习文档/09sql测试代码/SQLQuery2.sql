--top的应用
select top 3 * from Atable; 
--like模糊查询
select * from Atable where id Like '11%';
--通配符 %（代替整串字符）  _（代替某一字符）  []（代替一个字符的范围）  [^]（代替不在范围的字符）
select * from Atable where id Like '11%';
select * from Atable where id Like '11_5';
--select * from Atable where textX Like '^[XYZ]01';
--select * from Atable where textX Like '[^XYZ]02';
--in的应用
select * from Atable where id in(1101,1102,1104);
--between
select * from Atable where id between 1101 and 1104;
--别名的应用，即搜索结果显示的时候另起名
select id as '账户',password as '密码' from Atable;
--join表连接
select * from Atable a inner join Btable b on a.textZ=b.textZ;--内连接两个表符合on条件的循环合并打印
select * from Atable a full join Btable b on a.textZ=b.textZ;--全连接两个表符合on条件的循环合并打印，不符合条件的将缺失部分用null补全
select * from Atable a left join Btable b on a.textZ=b.textZ;--左连接在全连接的基础上，以左表为准，左边必须有数据
select * from Atable a right join Btable b on a.textZ=b.textZ;--右连接在全连接的基础上，以右表为准，右边必须有数据
--union应用，将两个语句的查询结果共同输出，但必须输出列数是相同的 union重复数据只输出一次，union all输出所有数据
select textX from Atable union select textZ from Btable;
select textX from Atable union all select textZ from Btable;
--primary key 主键    foreign key 外键   not null 不为空  unique 该列的值唯一  default 默认值  check 保证值符合条件
--NewTable_Id int FOREIGN KEY REFERENCES OldTable(OldTable_Id)
--索引的用法 将某表的一列或几列凑成一个索引
create index index01 on ATable(id,password);
--drop删除，alter修改

--列上自动创建主键值
--   id int IDENTITY(1,1) not null

--View视图的应用，视图类似于可视化的表，一般用于存结果集，视图一但创建，会随着源表数据的更新而更新
create VIEW viewo1 as select * from Atable;
select * from viewo1;
insert into Atable values(1111,'aaa','X08','Y08','Z08');
select * from viewo1;

--常用函数  
  --avg()   平均
  --count() 行数
  --first() 第一条数据
  --last()  最后一条数据
  --max()   最大值
  --mix()   最小值
  --sum()   求和
  
--group by分组    select的对象必须包含在聚合函数或者by中    聚合函数一般是上方内置的系统函数
select Atable.id,count(Atable.id) from Atable  
left join Btable
on Atable.id != Btable.id
group by Atable.id;
--having 过滤条件，用于弥补where，因为where不能与聚合函数一起使用
select Atable.id,count(Atable.id) from Atable  
left join Btable
on Atable.id != Btable.id
group by Atable.id
having count(Atable.id)>6;
--upper，lower   将字段转换大小写
select UPPER(password) from Atable;
select lower(password) from Atable;
--len 文本字段长度
select LEN(id) as 文本长度 from Atable;
--getdate 返回系统时间
select password,GETDATE() as date from Atable;
--convert  日期函数格式 convert（字段长度，获取日期，日期格式代号）   日期格式代号参考百度文档
select id,convert(varchar(100),GETDATE(),110) as date from Atable;
--自定义函数 function     创建 删除 调用
create function fuc01(@id01 int)
returns @table01 table(id02 int,password02 varchar(255))
as
begin
   insert @table01 select id,password from Atable where id=@id01
return
end;
drop function fuc01;
select * from fuc01(1101);
--自定义过程 procedure