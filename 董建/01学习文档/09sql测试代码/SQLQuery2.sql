--top��Ӧ��
select top 3 * from Atable; 
--likeģ����ѯ
select * from Atable where id Like '11%';
--ͨ��� %�����������ַ���  _������ĳһ�ַ���  []������һ���ַ��ķ�Χ��  [^]�����治�ڷ�Χ���ַ���
select * from Atable where id Like '11%';
select * from Atable where id Like '11_5';
--select * from Atable where textX Like '^[XYZ]01';
--select * from Atable where textX Like '[^XYZ]02';
--in��Ӧ��
select * from Atable where id in(1101,1102,1104);
--between
select * from Atable where id between 1101 and 1104;
--������Ӧ�ã������������ʾ��ʱ��������
select id as '�˻�',password as '����' from Atable;
--join������
select * from Atable a inner join Btable b on a.textZ=b.textZ;--���������������on������ѭ���ϲ���ӡ
select * from Atable a full join Btable b on a.textZ=b.textZ;--ȫ�������������on������ѭ���ϲ���ӡ�������������Ľ�ȱʧ������null��ȫ
select * from Atable a left join Btable b on a.textZ=b.textZ;--��������ȫ���ӵĻ����ϣ������Ϊ׼����߱���������
select * from Atable a right join Btable b on a.textZ=b.textZ;--��������ȫ���ӵĻ����ϣ����ұ�Ϊ׼���ұ߱���������
--unionӦ�ã����������Ĳ�ѯ�����ͬ����������������������ͬ�� union�ظ�����ֻ���һ�Σ�union all�����������
select textX from Atable union select textZ from Btable;
select textX from Atable union all select textZ from Btable;
--primary key ����    foreign key ���   not null ��Ϊ��  unique ���е�ֵΨһ  default Ĭ��ֵ  check ��ֵ֤��������
--NewTable_Id int FOREIGN KEY REFERENCES OldTable(OldTable_Id)
--�������÷� ��ĳ���һ�л��дճ�һ������
create index index01 on ATable(id,password);
--dropɾ����alter�޸�

--�����Զ���������ֵ
--   id int IDENTITY(1,1) not null

--View��ͼ��Ӧ�ã���ͼ�����ڿ��ӻ��ı�һ�����ڴ���������ͼһ��������������Դ�����ݵĸ��¶�����
create VIEW viewo1 as select * from Atable;
select * from viewo1;
insert into Atable values(1111,'aaa','X08','Y08','Z08');
select * from viewo1;

--���ú���  
  --avg()   ƽ��
  --count() ����
  --first() ��һ������
  --last()  ���һ������
  --max()   ���ֵ
  --mix()   ��Сֵ
  --sum()   ���
  
--group by����    select�Ķ����������ھۺϺ�������by��    �ۺϺ���һ�����Ϸ����õ�ϵͳ����
select Atable.id,count(Atable.id) from Atable  
left join Btable
on Atable.id != Btable.id
group by Atable.id;
--having ���������������ֲ�where����Ϊwhere������ۺϺ���һ��ʹ��
select Atable.id,count(Atable.id) from Atable  
left join Btable
on Atable.id != Btable.id
group by Atable.id
having count(Atable.id)>6;
--upper��lower   ���ֶ�ת����Сд
select UPPER(password) from Atable;
select lower(password) from Atable;
--len �ı��ֶγ���
select LEN(id) as �ı����� from Atable;
--getdate ����ϵͳʱ��
select password,GETDATE() as date from Atable;
--convert  ���ں�����ʽ convert���ֶγ��ȣ���ȡ���ڣ����ڸ�ʽ���ţ�   ���ڸ�ʽ���Ųο��ٶ��ĵ�
select id,convert(varchar(100),GETDATE(),110) as date from Atable;
--�Զ��庯�� function     ���� ɾ�� ����
create function fuc01(@id01 int)
returns @table01 table(id02 int,password02 varchar(255))
as
begin
   insert @table01 select id,password from Atable where id=@id01
return
end;
drop function fuc01;
select * from fuc01(1101);
--�Զ������ procedure