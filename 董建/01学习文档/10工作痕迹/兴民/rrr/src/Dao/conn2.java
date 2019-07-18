package Dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class conn2 {
   public List<model> Show(String str2) {
	   String strValue=str2;
	   System.out.println("数据连接为"+strValue);
	   //鍒涘缓涓�涓猰odel瀵硅薄
	   List<model> listTest = new ArrayList<model>();
	   int i=0;
	 //鍒涘缓杩炴帴
	   Connection conn = null ;
	  //鍒涘缓棰勭紪璇戝璞�
	   PreparedStatement st = null ;
	  //鍒涘缓缁撴灉闆�
	   ResultSet rs = null;
	   
	  
		
	  //鍒涘缓椹卞姩鍒濆鍖栧瓧绗︼紝濡傛灉鏄痬ysql鍒欐槸 com.mysql.jdbc.driver
	  String cl_orcl = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
	  //鍒涘缓杩炴帴璇彞
	  //兴民数据库
	  String str="jdbc:sqlserver://192.168.200.2;databaseName=fhadmin";
	    String username = "sa";
	    String password = "Xm123456";
//	    String str="jdbc:sqlserver://111.37.3.13:1433;databaseName=fhadmin";
//	    String username = "sa";
//	    String password = "123456";
	  //鍒涘缓sql璇彞
	    String strSql=" select top 5 te.ename 巡检点,te.ETYPE 巡检类型,te.status 状态,te.ELOCATION 巡检地点,te.ATTRIBUTE1 位置,tei.icontent 检查项目,teir.iresult 结果,su.name 巡检人姓名,teir.creation_date 检查时间,su2.name 处理人姓名,teir.chulidate 处理时间,teir.chulicuoshi 处理措施,teip.imageurl from TB_EQUIPMENT_ITEM tei\r\n" + 
	    		"	    join TB_EQUIPMENT_ITEM_RECORD teir\r\n" + 
	    		"	    on tei.EQUIPMENTITEM_ID=teir.igid\r\n" + 
	    		"	    join TB_EQUIPMENTINFO te\r\n" + 
	    		"	    on te.EQUIPMENTINFO_ID=tei.EQUIPMENTINFO_ID and te.ETYPE=?\r\n" + 
	    		"	    join sys_user su\r\n" + 
	    		"	    on su.username=teir.created_by_code\r\n" + 
	    		"	    left join sys_user su2\r\n" + 
	    		"	    on teir.chuliren=su2.username\r\n" + 
	    		"	    join TB_EQUIPMENT_IMG teip\r\n" + 
	    		"	    on teip.PGID=teir.gid";
	   
	    
	    //鍔犺浇椹卞姩
	    try {
			Class.forName(cl_orcl);
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    //杩炴帴鏁版嵁搴�
	    try {
			conn = DriverManager.getConnection(str,username,password);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    
	    //缂栬瘧瀵硅薄鑾峰彇
	    try {
	    	
			st = conn.prepareStatement(strSql);
			st.setString(1, strValue);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    
	    //鎼滅储缁撴灉鑾峰彇
	    try {
			rs = st.executeQuery();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    try {
			while(rs.next()){
				System.out.println(rs.getString(1)+"--"+rs.getString(2)+"--"+rs.getString(3)+"--"+rs.getString(4)+"--"+rs.getString(5)+"--"+rs.getString(6)+"--"+rs.getString(7)+"--"+rs.getString(8)+"--"+rs.getString(9)+"--"+rs.getString(10)+"--"+rs.getString(11)+"--"+rs.getString(12));
				model mo11 = new model();
				mo11.setEname(rs.getString(1));
				mo11.setEtype(rs.getString(2));
				mo11.setStatus(rs.getString(3));
				mo11.setElocation(rs.getString(4));
				mo11.setAttribute1(rs.getString(5));
				mo11.setIcontent(rs.getString(6));
				mo11.setIresult(rs.getString(7));
				mo11.setName(rs.getString(8));
				mo11.setCreation_date(rs.getString(9));
				mo11.setName2(rs.getString(10));
				mo11.setChulidate(rs.getString(11));
				mo11.setChulicuoshi(rs.getString(12));
				mo11.setImageurl(rs.getString(13));
				
				listTest.add(mo11);
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    try {
			rs.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    try {
			conn.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
//	    while(i<=listTest.size()) {
//	    	model model01 = listTest.get(i);
//	    	System.out.println(model01.getId()+"--"+model01.getName()+"--"+model01.getEmail());
//	    	i++;
//	    }
	    System.out.println(listTest.get(2).getChulicuoshi());
		return listTest;
   }
}
