<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="AddressBookNew.Pages.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagetitle">
        <h1>Profile</h1>
    </div>

    <div>
        <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
    </div>
    <!-- End Page Title -->

    <section class="section profile">
        <div class="row">
            <div class="col-xl-4">

                <div class="card">
                    <div class="card-body profile-card pt-4 d-flex flex-column align-items-center">
                        <asp:Image ID="imgBigProfile" AlternateText="Profile Image" CssClass="rounded-circle" Width="500px" runat="server" />
                        <%--<img src="assets/img/profile-img.jpg" alt="Profile" class="rounded-circle">--%>
                        <h2>
                            <asp:Label runat="server" ID="lblName" CssClass="col-lg-9 col-md-8"></asp:Label></h2>
                        <%--<h3>Web Designer</h3>--%>
                        <%--<div class="social-links mt-2">
                            <a href="#" class="twitter"><i class="bi bi-twitter"></i></a>
                            <a href="#" class="facebook"><i class="bi bi-facebook"></i></a>
                            <a href="#" class="instagram"><i class="bi bi-instagram"></i></a>
                            <a href="#" class="linkedin"><i class="bi bi-linkedin"></i></a>
                        </div>--%>
                    </div>
                </div>

            </div>

            <div class="col-xl-8">

                <div class="card">
                    <div class="card-body pt-3">
                        <!-- Bordered Tabs -->
                        <ul class="nav nav-tabs nav-tabs-bordered">

                            <li class="nav-item">
                                <asp:LinkButton class="nav-link active" data-bs-toggle="tab" data-bs-target="#profile-overview">Overview</asp:LinkButton>
                            </li>

                            <li class="nav-item">
                                <asp:LinkButton class="nav-link" data-bs-toggle="tab" data-bs-target="#profile-edit">Edit Profile</asp:LinkButton>
                            </li>

                            <li class="nav-item">
                                <asp:LinkButton class="nav-link" data-bs-toggle="tab" data-bs-target="#profile-change-password">Change Password</asp:LinkButton>
                            </li>

                        </ul>
                        <div class="tab-content pt-2">

                            <div class="tab-pane fade show active profile-overview" id="profile-overview">

                                <h5 class="card-title">Profile Details</h5>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label ">Full Name</div>
                                    <asp:Label runat="server" ID="lblUserName" CssClass="col-lg-9 col-md-8"></asp:Label>
                                    <%--<div class="col-lg-9 col-md-8">Kevin Anderson</div>--%>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Address</div>
                                    <asp:Label runat="server" ID="lblAddress" CssClass="col-lg-9 col-md-8"></asp:Label>
                                    <%--<div class="col-lg-9 col-md-8">A108 Adam Street, New York, NY 535022</div>--%>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Phone</div>
                                    <asp:Label runat="server" ID="lblMobileNo" CssClass="col-lg-9 col-md-8"></asp:Label>
                                    <%--<div class="col-lg-9 col-md-8">(436) 486-3538 x29071</div>--%>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Email</div>
                                    <asp:Label runat="server" ID="lblEmail" CssClass="col-lg-9 col-md-8"></asp:Label>
                                    <%--<div class="col-lg-9 col-md-8">k.anderson@example.com</div>--%>
                                </div>

                            </div>

                            <div class="tab-pane fade profile-edit pt-3" id="profile-edit">

                                <!-- Profile Edit Form -->
                                <div class="row mb-3">
                                    <label for="profileImage" class="col-md-4 col-lg-3 col-form-label">Profile Image</label>
                                      <div class="col-md-8 col-lg-9">
                                          <asp:Image ID="imgPreview" ToolTip="Profile Image" runat="server" />
                                        <div class="pt-2">
                                            <asp:FileUpload runat="server" ToolTip="Upload new profile image" id="fuProfilePhoto" accept=".jpg, .jpeg, .png"></asp:FileUpload>
                                          <%--<a href="#" class="btn btn-primary btn-sm" title="Upload new profile image"><i class="bi bi-upload"></i></a>--%>
                                          <%--<a href="#" class="btn btn-danger btn-sm" title="Remove my profile image"><i class="bi bi-trash"></i></a>--%>
                                        </div>
                                      </div>
                                </div>

                                <div class="row mb-3">
                                    <label for="fullName" class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <label for="Phone" class="col-md-4 col-lg-3 col-form-label">Mobile No.</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server"></asp:TextBox>
                                        <%--<input name="phone" type="text" class="form-control" id="Phone" value="(436) 486-3538 x29071">--%>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <label for="Email" class="col-md-4 col-lg-3 col-form-label">Email</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<input name="email" type="email" class="form-control" id="Email" value="k.anderson@example.com">--%>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <label for="Address" class="col-md-4 col-lg-3 col-form-label">Address</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<input name="address" type="text" class="form-control" id="Address" value="A108 Adam Street, New York, NY 535022">--%>
                                    </div>
                                </div>

                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click" Text="Save Changes" />
                                    <asp:Button runat="server" ID="btnCancel" class="btn btn-light" OnClick="btnCancel_Click" Text="Cancel" />
                                </div>
                                <!-- End Profile Edit Form -->

                            </div>

                            <div class="tab-pane fade pt-3" id="profile-change-password">
                                <!-- Change Password Form -->
                                <div class="row mb-3">
                                    <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label">Current Password</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtOldPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <label for="newPassword" class="col-md-4 col-lg-3 col-form-label">New Password</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                        <%--<input name="newpassword" type="password" class="form-control" id="newPassword">--%>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <label for="renewPassword" class="col-md-4 col-lg-3 col-form-label">Re-enter New Password</label>
                                    <div class="col-md-8 col-lg-9">
                                        <asp:TextBox ID="txtRetypePassword" CssClass="form-control" runat="server"></asp:TextBox>
                                        <%--<input name="renewpassword" type="password" class="form-control" id="renewPassword">--%>
                                    </div>
                                </div>

                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSavePassword" type="submit" class="btn btn-primary" OnClick="btnSavePassword_Click" Text="Change Password" />
                                </div>
                                <!-- End Change Password Form -->

                            </div>

                        </div>
                        <!-- End Bordered Tabs -->

                    </div>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
