﻿<%@ Page Title="Student Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="InternshipER.studentForm" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Company.css">
    <link rel="stylesheet" type="text/css" href="Content/Student.css">
    
   <script src="Scripts/Student.js"></script>
   <script src="https://use.fontawesome.com/bfdd1d98a1.js"></script>
<div class="container">
	<div class="row">
		<section id="about" class="section section-about wow fadeInUp" style="visibility: visible; animation-name: fadeInUp;">
					<div class="profile">
						<div class="row">
							<div class="col-sm-3">
								<div class="photo-profile">
									<img id="img-profile" src="img/logo2.png">
								</div>
								<a href="cv/cv-1.pdf" target="cv">
									<div class="download-resume">
										<i class="fa fa-check" aria-hidden="true"></i>
										<span class="text-download">CV indir</span>
									</div>
								</a>
									<div class="download-resume">
										<button type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">
                                                    Profil Düzenle
                                         </button>
									</div>
                                
							</div>
							<div class="col-sm-8">
								<div class="info-profile">
									<h2><span>HI I'M</span> <asp:Label ID="studentName" runat="server" class="info" Text="öğrenci ismi"></asp:Label></h2>
									<h3>
                                            <asp:Label ID="studentDepartment" runat="server" class="info" Text="öğrenci bölümü"></asp:Label>
                                     </h3>
									<p align="justify">
                                            <asp:Label ID="studentDescription" runat="server" class="info" Text="öğrenci açıklaması"></asp:Label>
                                     </p>
									<div class="row">
										<div class="col-sm-6">
											<ul class="ul-info">
												<li class="li-info">
													<span class="title-info">Yas</span>
                                                     <asp:Label ID="studentAge" runat="server" class="info" Text="Yas"></asp:Label>
												</li>
												<li class="li-info">
													<span class="title-info">Adres</span>
                                                    <asp:Label ID="studentAdress" runat="server" class="info" Text="Adres"></asp:Label>
												</li>
												<li class="li-info">
													<span class="title-info">E mail</span>
                                                     <asp:Label ID="studentEmail" runat="server" class="info" Text="Email"></asp:Label>
												</li>
											</ul>
										</div>


										<div class="col-sm-6">
											<ul class="ul-info">
												<li class="li-info">
													<span class="title-info">Telefon</span>
                                                     <asp:Label ID="studentPhone" runat="server" class="info" Text="05xx xxx xx xx"></asp:Label>
												</li>
												<li class="li-info">
													<span class="title-info">Web sitesi</span>
                                                     <asp:Label ID="studentWebsite" runat="server" class="info" Text="www.ogrenci.tr"></asp:Label>
												</li>
												<li class="li-info">
													<span class="title-info">Ülke</span>
                                                     <asp:Label ID="studentCountry" runat="server" class="info" Text="Türkiye"></asp:Label>
												</li>
                                                <li class="li-info">
													<span class="title-info">Okul</span>
                                                     <asp:Label ID="studentSchool" runat="server" class="info" Text="okul"></asp:Label>
												</li>
											</ul>
										</div>
                                        <div class="row" style="margin-top: 180px; background-color="">
                                        <div class="col-md-12">
                                            <div class="well well-sm">
                                                <div class="text-right">
                                                    <a class="btn btn-success btn-green" href="#reviews-anchor" id="open-review-box">Değerlendirme Yaz</a>
                                                </div>

                                                <div class="row" id="post-review-box" style="display: none;">
                                                    <div class="col-md-12">
                                                        <form accept-charset="UTF-8" action="" method="post">
                                                            <input id="ratings-hidden" name="rating" type="hidden">
                                                            <textarea class="form-control animated" cols="150" id="new-review" name="comment" placeholder="Değerlendirmenizi buraya giriniz..." rows="15"></textarea>

                                                            <div class="text-right">
                                                                <div class="stars starrr" data-rating="0"></div>
                                                                <a class="btn btn-danger btn-sm" href="#" id="close-review-box" style="display: none; margin-right: 10px;">
                                                                    <span class="glyphicon glyphicon-remove"></span>Vazgeç</a>
                                                                <button class="btn btn-success btn-lg" type="submit">Kaydet</button>
                                                            </div>
                                                        </form>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="reviews">
                                        <div class="col-md-5 blockquote review-item">
                                            <div class="col-md-3 text-center">
                                                <img class="rounded-circle reviewer" src="http://standaloneinstaller.com/upload/avatar.png">
                                                <div class="caption">
                                                    <small>by<a href="#reviewer"> <asp:Label ID="labelname4" runat="server" Text="Label"></asp:Label></a></small>
                                                </div>

                                            </div>
                                            <div class="col-md-9">
                                                <h4><asp:Label ID="labelname1" runat="server" Text="Label"></asp:Label></h4>
                                                <div class="ratebox text-center" data-id="0" data-rating="5"></div>
                                                <p class="review-text"><asp:Label ID="labelname2" runat="server" Text="Label"></asp:Label></p>
                                                <small class="review-date"><asp:Label ID="labelname3" runat="server" Text="Label"></asp:Label></small>
                                            </div>
                                        </div>
                                        <div class="col-md-5 blockquote review-item">
                                            <div class="col-md-3 text-center">
                                                <img class="rounded-circle reviewer" src="http://standaloneinstaller.com/upload/avatar.png">
                                                <div class="caption">
                                                    <small>by<a href="#reviewer"> <asp:Label ID="labelname8" runat="server" Text="Label"></asp:Label></a></small>
                                                </div>

                                            </div>
                                            <div class="col-md-9">
                                                <h4><asp:Label ID="labelname5" runat="server" Text="Label"></asp:Label></h4>
                                                <div class="ratebox text-center" data-id="0" data-rating="5"></div>
                                                <p class="review-text"><asp:Label ID="labelname6" runat="server" Text="Label"></asp:Label></p>
                                                <small class="review-date"><asp:Label ID="labelname7" runat="server" Text="Label"></asp:Label></small>
                                            </div>
                                        </div>
                                    </div>

										<div class="col-sm-12">
											<span class="title-links">&nbsp;&nbsp;&nbsp;Sosyal Platformlar</span>
											<ul class="ul-social-links">
												<li class="li-social-links">
													<a href="www.facebook.com/shineblue30" data-tootik="Facebook" data-tootik-conf="square"><i class="fa fa-facebook" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="www.twitter.com/shineblue30" data-tootik="Twitter" data-tootik-conf="square"><i class="fa fa-twitter" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="#" data-tootik="Google Plus" data-tootik-conf="square"><i class="fa fa-google-plus" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="https://www.linkedin.com/in/shineblue30/" data-tootik="Linkedin" data-tootik-conf="square"><i class="fa fa-linkedin" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="#" data-tootik="Dribbble" data-tootik-conf="square"><i class="fa fa-dribbble" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="#" data-tootik="Pinterest" data-tootik-conf="square"><i class="fa fa-pinterest-p" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="#" data-tootik="Vimeo" data-tootik-conf="square"><i class="fa fa-vimeo" aria-hidden="true"></i></a>
												</li>
												<li class="li-social-links">
													<a href="#" data-tootik="Behance" data-tootik-conf="square"><i class="fa fa-behance" aria-hidden="true"></i></a>
												</li>
											</ul>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</section>
	</div>
</div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Profil Düzenleme</h4>
                </div>
                <div class="modal hide" id="myModal">

                    <button type="button" class="close" data-dismiss="modal">x</button>
                    <h3>Login to MyWebsite.com</h3>
                </div>
                <div class="modal-body" align="center">
                    <div class="container">
                        <div class="row">
                            <form method="post" action='' name="login_form" align="center">
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="usr">Öğrenci ismi:</label>
                                        <input type="text" runat="server" class="form-control" id="studentNameEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Öğrenci Bölümü:</label>
                                        <input type="text" runat="server" class="form-control" id="studentDepartmentEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Öğrencinin Yaş:</label>
                                        <input type="text" runat="server" class="form-control" id="studentAgeEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Öğrencini Ülkesi:</label>
                                        <input type="text" runat="server" class="form-control" id="studentCountryEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="comment">Öğrenci Hakkında:</label>
                                        <textarea runat="server" class="form-control" rows="5" id="studentDescriptionEdit"></textarea>
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Adres:</label>
                                        <input type="text" runat="server" class="form-control" id="studentAdressEdit">
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label for="usr">Web Sitesi:</label>
                                        <input type="text" runat="server" class="form-control" id="studentWebsiteEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Telefon:</label>
                                        <input type="text" runat="server" class="form-control" id="studentPhoneEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Email:</label>
                                        <input type="text" runat="server" class="form-control" id="studentEmailEdit">
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Okul:</label>
                                        <input type="text" runat="server" class="form-control" id="studentSchoolEdit">
                                    </div>
                                    <div class="form-group">
                                        <span class="form-group-btn">
                                            <span class="btn btn-primary btn-file">Photo&hellip;
                                    <input type="file" single>
                                            </span>
                                        </span>
                                        <input type="text" runat="server" id="studentPhotoEdit" class="form-control" readonly>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <<div class="modal-footer">
                    <asp:Button class="btn btn-primary" type="submit" runat="server" ID="submitSettings" OnClick="updateStudentProfile" CausesValidation="False" OnDataBinding="updateStudentProfile" UseSubmitBehavior="False" Text="Kaydet"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
