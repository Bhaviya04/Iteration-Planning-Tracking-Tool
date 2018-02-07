<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Backend_login" EnableEventValidation="false" %>

<html lang="en" class="body-full-height">
    
<!-- Mirrored from aqvatarius.com/themes/atlant/html/pages-login.html by HTTrack Website Copier/3.x [XR&CO'2013], Wed, 27 May 2015 07:58:38 GMT -->
<!-- Added by HTTrack --><meta http-equiv="content-type" content="text/html;charset=utf-8" /><!-- /Added by HTTrack -->
<head>        
        <!-- META SECTION -->
        <title>BA Tool - Planning & Estimation Software</title>            
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        
        <link rel="icon" href="favicon.ico" type="image/x-icon" />
        <!-- END META SECTION -->
        
        <!-- CSS INCLUDE -->        
        <link rel="stylesheet" type="text/css" id="theme" href="css/theme-default.css"/>
        <!-- EOF CSS INCLUDE -->    
    </head>
    <body>
        <form id="form1" runat="server" class="form-horizontal">
        <div class="login-container">
       
            <div class="login-box animated fadeInDown">
                
                <div class="login-logo"><center><strong style="color: #FFFFFF; font-size: xx-large">Planning & Estimation</strong></center></div>
                <div class="login-body">
                    <div class="login-title"><strong>Welcome to BA Tool</strong>, Please login</div>
                   
                    <div class="form-group">
                        <div class="col-md-12">
                       
                            <asp:TextBox ID="txtusername" runat="server" class="form-control" placeholder="Username"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvusername" runat="server" ValidationGroup="r" ErrorMessage="* Required" ControlToValidate="txtusername" ForeColor="Red" Display="Dynamic" Font-Size="Small"></asp:RequiredFieldValidator>
          
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                           
                             <asp:TextBox ID="txtpassword" runat="server" class="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvpassword" runat="server" ValidationGroup="r" ErrorMessage="* Required" ControlToValidate="txtpassword" ForeColor="Red" Display="Dynamic" Font-Size="Small"></asp:RequiredFieldValidator>
          
                        </div>
                    </div>
                    <div class="form-group">

                     <div class="col-md-6">
                           
                        </div>
                       
                        <div class="col-md-6">
                         
                            <asp:Button ID="btn_save" runat="server" class="btn btn-primary btn-rounded pull-right" 
                           ValidationGroup="r" OnClick="Save_Click" Text="LOG IN" />
                           
                        </div>

                    </div>


                     <div class="form-group">

                     <div class="col-md-12">
                          <asp:Label ID="lbldisplay" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                       
                       

                    </div>


                </div>
                

                <div class="form-group">
                    
                     <div class="col-md-12">
                         <br/><br/>
                         <center><strong style="color: #FFFFFF; font-size: xx-large">By Bhaviya & Akash</strong></center>
                        </div>
                       
                       

                    </div>
               
            </div>
            
        </div>
    
    <!-- COUNTERS // NOT INCLUDED IN TEMPLATE -->
        <!-- GOOGLE -->
        <script type="text/javascript">
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
          m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '../../../../www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-36783416-1', 'aqvatarius.com');
            ga('send', 'pageview');
        </script>        
        <!-- END GOOGLE -->
        
        <!-- YANDEX -->
        <script type="text/javascript">
            (function (d, w, c) {
                (w[c] = w[c] || []).push(function () {
                    try {
                        w.yaCounter25836617 = new Ya.Metrika({ id: 25836617,
                            webvisor: true,
                            accurateTrackBounce: true
                        });
                    } catch (e) { }
                });

                var n = d.getElementsByTagName("script")[0],
                s = d.createElement("script"),
                f = function () { n.parentNode.insertBefore(s, n); };
                s.type = "text/javascript";
                s.async = true;
                s.src = (d.location.protocol == "https:" ? "https:" : "http:") + "//mc.yandex.ru/metrika/watch.js";

                if (w.opera == "[object Opera]") {
                    d.addEventListener("DOMContentLoaded", f, false);
                } else { f(); }
            })(document, window, "yandex_metrika_callbacks");
        </script>
        <noscript><div><img src="http://mc.yandex.ru/watch/25836617" style="position:absolute; left:-9999px;" alt="" /></div></noscript>     
        <!-- END YANDEX -->
    <!-- END COUNTERS // NOT INCLUDED IN TEMPLATE -->
        </form>
    </body>

<!-- Mirrored from aqvatarius.com/themes/atlant/html/pages-login.html by HTTrack Website Copier/3.x [XR&CO'2013], Wed, 27 May 2015 07:58:38 GMT -->
</html>