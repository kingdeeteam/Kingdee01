<%@page import="org.apache.jasper.tagplugins.jstl.core.Import"%>
<%@page import="Dao.conn"%>
<%@page import="Dao.DownExcel"%>
<%@page import="java.util.List"%>
<%@page import="Dao.model"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Insert title here</title>
<style>
			/*.divcss5
			   {
			    margin-left:150px;
				margin-right:150px; 
				width:100%px;
				height:100%px;
				border:1px solid #f00;
				padding-left:130px;
				padding-bottom:20px;
				}
				a{text-align:center} */
				
				#tab{color:balck;font-size:50;font-family:宋体;text-algn:center;background-color:背景颜色}
				
				
</style>
</head>
<body>


<div class="divcss5">
  <%
  	conn conn = new conn();
    List<model> downExcel2 = conn.Show();
  %>
  <table border="1" id="tab"><tr>
  <th>巡检点</th>
  <th>巡检类型</th>
  <th>状态</th>
  <th>巡检地点</th>
  <th>位置</th>
  <th>检查项目</th>
  <th>结果</th>
  <th>巡检人姓名</th>
  <th>巡查时间</th>
  <th>处理人姓名</th>
  <th>处理时间</th>
  <th>处理措施</th>
  <th>img</th>
  </tr>
  <% 
  //downExcel2.size()
  int i=0;
  while(i<40) {
  out.println("<tr><th>"+downExcel2.get(i).getEname()+
		  "</th>"+"<th>"+downExcel2.get(i).getEtype()+
		  "</th>"+"<th>"+downExcel2.get(i).getStatus()+
		  "</th>"+"<th>"+downExcel2.get(i).getElocation()+
		  "</th>"+"<th>"+downExcel2.get(i).getAttribute1()+
		  "</th>"+"<th>"+downExcel2.get(i).getIcontent()+
		  "</th>"+"<th>"+downExcel2.get(i).getIresult()+
		  "</th>"+"<th>"+downExcel2.get(i).getName()+
		  "</th>"+"<th>"+downExcel2.get(i).getCreation_date()+
		  "</th>"+"<th>"+downExcel2.get(i).getName2()+
		  "</th>"+"<th>"+downExcel2.get(i).getChulidate()+
		  "</th>"+"<th>"+downExcel2.get(i).getChulicuoshi()+
		  "</th>"+"<th>"+"<img style='height:200px;width:200px;' src='data:image/jpg;base64,"+downExcel2.get(i).getImageurl()+"' />"+"</th></tr>");
    i++;
  }
  %>
  </table>
  </div>
</body>
</html>