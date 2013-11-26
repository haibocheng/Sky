<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GreedyPlayer.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var reverseService = function () {
            this.sayHello = function () {
                alert('1');
            }
        };

        var testFactory = function () {
            return {
                sayHello: function () {
                    alert('1');
                }
            }
        };

        var a = new reverseService();
        var b = reverseService();

        var c = new testFactory();
        var d = testFactory();
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
