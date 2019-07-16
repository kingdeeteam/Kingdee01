<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Insert title here</title>
<style>
			.divcss5
			   {
			    margin-left:150px;
				margin-right:150px; 
				width:100%px;
				height:100px;
				border:1px solid #f00;
				padding-left:130px;
				padding-bottom:20px;
				}
				
	
</style>
</head>
<body>
   <div style="margin-top:50px;margin-left:500px;">
   <a style=" color:#666; font-size:32px;letter-spacing:20PX">巡      检       报      表     查      看</a>
   </div>
    <div style="margin-top:50px;margin-left:580px;">
   <form action="test" method="post">
      <input type="submit" value="全部查询" style="width: 95px; height: 40px;">
      
   </form>
   </div>
   
    
   <div style="margin-top:50px;margin-left:580px;height: 350px;">
    <form action="test2" method="post">
   <select name="sele"  style="width:150px;height:40px;font-size=64px;">
      <option>现场5s</option>
      <option>违规违纪</option>
      <option>设备检查</option>
      <option>跑冒滴漏</option>
      <option>属地管理</option>
      <option>锅炉日常巡检</option>
   </select>
     <input type="submit" value="查       询" style="width: 95px; height: 40px;">
      
   </form>
</div>

   


</body>
</html>