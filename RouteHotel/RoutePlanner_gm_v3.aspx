<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePlanner_gm_v3.aspx.cs" Inherits="RouteHotel.RoutePlanner_gm_v3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route planner</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <link href="RoutePlanner.css" rel="stylesheet" type="text/css" />
    <style>
      
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true"></script>
    <script src="/scripts/RouteHotel.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">    
        <div id ="routeSearchParams">
            TODO: add search params here
        </div>
        <div id="map-canvas" ></div> 
    </form>
</body>
</html>
