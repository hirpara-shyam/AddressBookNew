<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AddressBookNew.Pages.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <title>Register - AddressBook</title>
    <meta content="" name="description">
    <meta content="" name="keywords">

    <!-- Favicons -->
    <link href="~/Content/assets/img/favicon.png" rel="icon">
    <link href="~/Content/assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.gstatic.com" rel="preconnect">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <!-- Vendor CSS Files -->
    <link href="~/Content/assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="~/Content/assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="~/Content/assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
    <link href="~/Content/assets/vendor/quill/quill.snow.css" rel="stylesheet">
    <link href="~/Content/assets/vendor/quill/quill.bubble.css" rel="stylesheet">
    <link href="~/Content/assets/vendor/remixicon/remixicon.css" rel="stylesheet">
    <link href="~/Content/assets/vendor/simple-datatables/style.css" rel="stylesheet">

    <!-- Template Main CSS File -->
    <link href="~/Content/assets/css/style.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <main>
            <div class="container">

                <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

                                <div class="d-flex justify-content-center py-4">
                                    <a href="index.html" class="logo d-flex align-items-center w-auto">
                                        <img src="~/Content/assets/img/logo.png" alt="">
                                        <span class="d-none d-lg-block">AddressBook</span>
                                    </a>
                                </div>
                                <!-- End Logo -->

                                <div class="card mb-3">

                                    <div class="card-body">

                                        <div class="pt-4 pb-2">
                                            <h5 class="card-title text-center pb-0 fs-4">Create an Account</h5>
                                            <p class="text-center small">Enter your personal details to create account</p>
                                        </div>

                                        <form class="row g-3 needs-validation" novalidate>
                                            <div class="col-12">
                                                <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="form-label"></asp:Label>
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter User name..."></asp:TextBox>
                                                <div class="invalid-feedback">Please, enter your name!</div>
                                            </div>

                                            <div class="col-12">
                                                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email..."></asp:TextBox>
                                                <div class="invalid-feedback">Please enter a valid Email adddress!</div>
                                            </div>

                                            <div class="col-12">
                                                <asp:Label ID="lblMobileNo" runat="server" Text="MobileNo" CssClass="form-label"></asp:Label>
                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile number..."></asp:TextBox>
                                                <div class="invalid-feedback">Please enter a valid Mobile Number!</div>
                                            </div>

                                            <div class="col-12">
                                                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="form-label"></asp:Label>
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Your Address..."></asp:TextBox>
                                                <div class="invalid-feedback">Please enter Address!</div>
                                            </div>

                                            <div class="col-12">
                                                <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="form-label"></asp:Label>
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password..."></asp:TextBox>
                                                <div class="invalid-feedback">Please enter your password!</div>
                                            </div>

                                            <hr />

                                            <div class="col-12">
                                                <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary w-100" Text="Create Account" OnClick="btnRegister_Click" />
                                            </div>
                                            <div class="col-12">
                                                <p class="small mb-0">
                                                    Already have an account?
                                                    <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="~/Pages/Login">Login</asp:HyperLink>
                                                </p>
                                            </div>

                                            <div class="col-12">
                                                <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
                                            </div>
                                        </form>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </section>

            </div>
        </main>
        <!-- End #main -->

        <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
    </form>

    <!-- Vendor JS Files -->
    <script src="/Content/assets/vendor/apexcharts/apexcharts.min.js"></script>
    <script src="/Content/assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="/Content/assets/vendor/chart.js/chart.umd.js"></script>
    <script src="/Content/assets/vendor/echarts/echarts.min.js"></script>
    <script src="/Content/assets/vendor/quill/quill.js"></script>
    <script src="/Content/assets/vendor/simple-datatables/simple-datatables.js"></script>
    <script src="/Content/assets/vendor/tinymce/tinymce.min.js"></script>
    <script src="/Content/assets/vendor/php-email-form/validate.js"></script>

    <!-- Template Main JS File -->
    <script src="/Content/assets/js/main.js"></script>
</body>
</html>
