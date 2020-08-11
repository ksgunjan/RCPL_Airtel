﻿$(document).ready(function () {

    //Sidebar Toggle
    $('.btn-nav-toggle').click(function () {
        $('.left-sidebar').toggleClass('sidebar-collapse');
        $('.site-holder').toggleClass('mini-sidebar');
        $('.hidden-minibar').toggleClass('hide');
        $('.toggle-left').toggleClass('rotate-toggle-btn');
        return false;
    });
    //Slide Sidebar in Responsive
    $(".btn-nav-toggle-responsive").click(function () {
        $(".left-sidebar").toggleClass("slide-leftbar")
    });
    //Slide Sidebar Nav
    $(".sidebar-holder .parent-nav > a").click(function () {
        $(this).next().slideToggle();
        $(this).parents('.parent-nav').siblings().find('.parent-nav-child').slideUp();
        //$(this).siblings('.parent-nav-child').slideUp();

        $(this).find('.fa-angle-down').toggleClass('rotate-icon');
    });
    // Add Current Page in Sidebar
    //var CURRENT_URL= window.location.href;
    var CURRENT_URL = window.location.href.split('/').slice(3).join('/');
    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="' + CURRENT_URL + '"]').parents(".parent-nav-child").slideDown();
    $Sidebar.find('a[href="' + CURRENT_URL + '"]').parents(".parent-nav-child").addClass('active').siblings(".parent-nav-child").removeClass("active");
    $Sidebar.find('a[href="' + CURRENT_URL + '"]').addClass('active');
    console.log(CURRENT_URL);
    //Table Accordian
    $(document).on('click', '.toggle-table-plus', function () {
        $(this).closest("tr").after("<tr class='clone-row'><td colspan='7' style='padding:0;'>" + $(this).next().html() + "</td></tr>")
        $(this).hide();
        $(this).prev('.toggle-table-minus').show();

    });
    $(document).on('click', '.toggle-table-minus', function () {
        $(this).parents('tr').next('.clone-row').remove();
        $(this).hide();
        $(this).next('.toggle-table-plus').show();
    });
    //Alert pop up box   
    function ErrorMssgPopup(data) {
        $("body").addClass('CaptchaError');
        $("#alertPopup").show();
        $("#alertPopup .alertMsg").text("");
        $("#alertPopup .alertMsg").append(data);
        return false;
    }
    //Hide Alert Pop up
    $('.close_alert').on('click', function (e) {
        $("body").css('overflow', 'visible');
        $('.alert-overlay-error').hide();
        e.preventDefault();
    });
    function SuccessfullPop(data) {
        $("body").addClass('CaptchaError');
        $("#alertPopupS").show();
        $("#alertPopupS .alertMsg").text("");
        $("#alertPopupS .alertMsg").append(data);
        return false;
    }
    //Hide Alert Pop up
    $('.close_alert').on('click', function () {
        $("body").css('overflow', 'visible');
        $('.alert-overlay-successful').hide();
    });
    //Show Hide Contact Details
    $('body').on('click', '.showMoreLink', function () {
        $(this).parents('.section-pannel').find('.contactFormRow').slideToggle();
        var $ToggleText = $(this).text();

        if ($ToggleText === "Show Details") {
            $($(this)).text('Hide Details');
        }
        else {
            $($(this)).text('Show Details');
        }
    });
    //Show Map
    $('body').on('click', '.showMap', function () {
        $('.map-box').show();
    });
    //Password Show on Click
    $(".toggle-password").on('click', function () {

        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $(".passField");
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
    //image Uplodad Functionality
    var count = 0;
    function handleFileSelect(evt) {
        var $fileUpload = $("input#files[type='file']");
        count = count + parseInt($fileUpload.get(0).files.length);

        if (parseInt($fileUpload.get(0).files.length) > 8 || count > 9) {
            alert("You can only upload a maximum of 8 files");
            count = count - parseInt($fileUpload.get(0).files.length);
            evt.preventDefault();
            evt.stopPropagation();
            return false;
        }
        var files = evt.target.files;
        for (var i = 0, f; f = files[i]; i++) {
            if (!f.type.match('image.*')) {
                continue;
            }
            var reader = new FileReader();
            reader.onload = (function (theFile) {
                return function (e) {
                    var span = document.createElement('span');
                    span.innerHTML = ['<img class="thumb" src="', e.target.result, '" title="', escape(theFile.name), '"/><span class="remove_img_preview"></span>'].join('');
                    document.getElementById('list').insertBefore(span, null);
                };
            })(f);

            reader.readAsDataURL(f);
        }
    }
    $('#files').change(function (evt) {
        handleFileSelect(evt);
    });
    $('#list').on('click', '.remove_img_preview', function () {
        $(this).parent('span').remove();
        //           parseInt($fileUpload.get(0).files.length - 1;
    });
    //Tooltip
    $('[data-toggle="tooltip"]').tooltip();
    // bootstrap Accordion closing parent
    $("body").on('click', '.faq-secion .accordion .card-header h2', function () {
        console.log('testing');
        $(this).parents('.card').siblings().find('.collapse').removeClass('in');
    });
    //Select 2 Dropwdown
    $(document).ready(function () {
        BindControls();
    });
    function BindControls() {
        $("#ContentPlaceHolder1_ddlproduct").select2({});
        $("#ContentPlaceHolder1_ddlcasestatus").select2({});
        $("#ContentPlaceHolder1_ddlproducttype").select2({});
        $("#ContentPlaceHolder1_ddlcompany").select2({});
    }
    var req = Sys.WebForms.PageRequestManager.getInstance();
    req.add_endRequest(function () {
        BindControls();
    });
    //DataTable Jquery
    $(function () { BindGrid(); });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest
            (function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) { BindGrid(); }
                else { BindGrid(); }
            });
    };
    function BindGrid() {
        $('#ContentPlaceHolder1_gvdetailleasedline').DataTable();
        $('#ContentPlaceHolder1_gvdetailpri').DataTable();
        $('#ContentPlaceHolder1_gvcomp').DataTable();
        $('#ContentPlaceHolder1_gvexcel').DataTable();
        $('#ContentPlaceHolder1_gvfedetaillist').DataTable();
        $('#ContentPlaceHolder1_gvll').DataTable();
        $('#ContentPlaceHolder1_gvmo').DataTable();
        $('#ContentPlaceHolder1_gvpri').DataTable();
        $('#ContentPlaceHolder1_gvnum').DataTable();
        $('#ContentPlaceHolder1_gvdetail').DataTable();

    }
    // document.multiselect('#ddlenduser');
    $(function () {
        $('[id*=ddlenduser]').multiselect({
            includeSelectAllOption: true
        });
    });





});



