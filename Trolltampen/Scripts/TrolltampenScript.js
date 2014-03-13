$(document).ready(function () {
    $.ajax({
        url: $("#articlemenu").attr("data-url"),
        dataType: "html",
        cache: false,
        success: function (data) {
            $("#articlemenu").html(data);
        }
    });
});

$(function () {
    $('#side-menu').metisMenu();
});

//Loads the correct sidebar on window load
$(function () {

    $(window).bind("load", function () {
        console.log($(this).width())
        if ($(this).width() < 768) {
            $('div.sidebar-collapse').addClass('collapse')
        } else {
            $('div.sidebar-collapse').removeClass('collapse')
        }
    })
})

//Collapses the sidebar on window resize
$(function () {

    $(window).bind("resize", function () {
        console.log($(this).width())
        if ($(this).width() < 768) {
            $('div.sidebar-collapse').addClass('collapse')
        } else {
            $('div.sidebar-collapse').removeClass('collapse')
        }
    })
})

//categoty list initiating
function InitiateCategoryList() {
    $("#page-wrapper").on("click", "#createcat", function () {
        window.location.href = $(this).attr("data-url");
    });
    InitiateList();
}

//Articles list initiating
function InitiateArticlesList() {
    $("#page-wrapper").on("click", "#createart", function () {
        window.location.href = $(this).attr("data-url");
    });
    InitiateList();
}


function InitValidation() {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");
};

//Preload images and fill forms for upload images on server
function ReadImage(file, fileNum) {
    if (fileNum === null) {
        fileNum = "";
    }
    var reader = new FileReader();
    var image = new Image();
    reader.onload = function (event) {
        image.src = event.target.result;
        image.onload = function (event) {
            $("#photoimg" + fileNum).attr("src", event.target.src);
            $("#photoimg" + fileNum).css("display", "block");
            $("#delphoto" + fileNum).css("display", "block");
            $("#photofilename" + fileNum).val(new Date().getTime() + file.name);
            $("#delphoto" + fileNum).css("display", "block");
            if (fileNum !== "") {
                var items = $(".galleryitem").length;
                var next = (Number(fileNum) + 1);
                if (next === items) {
                    var order = next.toString();
                    $("#photogallery").append("<div class='form-group galleryitem' id='galleryitem" + order + "'>" +
                        "<label class='control-label col-md-4'>Add photo to gallery</label>" +
                            "<div class='col-md-6'>" +
                                "<input type='file' data-id='" + order + "' class='form-control galleryfile' id='photofile" + order + "' name='Content.Gallery.PhotoFiles[" + order + "].PhotoFile'/>" +
                                "<input type='hidden' id='photofilename" + order + "' name='Content.Gallery.PhotoFiles[" + order + "].FileName'>" +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<img id='photoimg" + order + "' width='180' height='120' style='display:none' />" +
                            "</div>" +
                            "<div class='form-group'>" +
                                "<input type='button' class='btn btn-default delgalleryfile' data-id='" + order + "' id='delphoto" + order + "' style='display:none' value='Delete picture' />" +
                            "</div>" +
                        "</div>");
                }
            }
        };
        image.onerror = function () {
            $("#photofile" + fileNum).val(null);
            $("#photoimg" + fileNum).attr("src", null);
            $("#delphoto" + fileNum).css("display", "none");
            alert("Wrong file type.");
        }
    }
    reader.readAsDataURL(file);

}

//Initiate DOM elements for working with images 
function InitiateCreatingContent() {
    $("#page-wrapper").on("change", "#mediaselector", function (event) {
        var selection = $(this).val();
        switch (selection) {
            case "1":
                ClearAllMedia()
                $("#photomedia").css("display", "block");
                $("#photogallery").css("display", "none");
                $("#videolink").css("display", "none");
                break;
            case "3":
                ClearAllMedia()
                $("#photomedia").css("display", "none");
                $("#photogallery").css("display", "block");
                $("#videolink").css("display", "none");
                break;
            case "2":
                ClearAllMedia()
                $("#photomedia").css("display", "none");
                $("#photogallery").css("display", "none");
                $("#videolink").css("display", "block");
                break;
            case "4":
                ClearAllMedia()
                $("#photomedia").css("display", "none");
                $("#photogallery").css("display", "none");
                $("#videolink").css("display", "none");
                break;
            default:
                break;
        }
        InitValidation();
    });
    $("#mediaselector").trigger("change");
    InitiateMediaPhoto();
    InitiateMediaGallery();
    InitiateFormWithContent();
}

//Delete info about preload image before uploading
function ClearPhotoMediaData(fileNum) {
    if (fileNum === null) fileNum = "";
    $("#photofile" + fileNum).val(null);
    $("#photoimg" + fileNum).attr("src", null);
    $("#photoimg" + fileNum).css("display", "none");
    $("#delphoto" + fileNum).css("display", "none");
    $("#photofilename" + fileNum).val(null);
}

//Initiate DOM elements for working with photo image
function InitiateMediaPhoto() {
    $("#page-wrapper").on("click", "#delphoto", function (e) {
        ClearPhotoMediaData(null);
    });
    $("#page-wrapper").on("change", "#photofile", function (e) {
        if (this.disabled) return alert("File upload is not supported.");
        var files = this.files;
        if (files && files[0]) {
            ReadImage(files[0], null);
        }
    });
}

////Initiate DOM elements for working image gallery
function InitiateMediaGallery() {
    $("#page-wrapper").on("change", ".galleryfile", function (e) {
        if (this.disabled) return alert("File upload is not supported.");
        var files = this.files;
        if (files && files[0]) {
            ReadImage(files[0], $(this).attr("data-id"));
        }
    });
    $("#page-wrapper").on("click", ".delgalleryfile", function () {
        var order = Number($(this).attr("data-id"));
        //ClearPhotoMediaData(order);
        $("#galleryitem" + order).remove();
        CheckGalleryItems();
    });
}

//Reordering form elements if some image delete from gallery before uploading
function CheckGalleryItems() {
    var $elements = $(".galleryitem");
    if ($elements.length > 0) {
        $($elements).each(function (index) {
            $(this).attr("id", "galleryitem" + index);
            $(this).find("input[type=file]").attr({ "data-id": index, "id": "photofile" + index, "name": "Content.Gallery.PhotoFiles[" + index + "].PhotoFile" });
            $(this).find("input[type=hidden]").attr({ "id": "photofilename" + index, "name": "Content.Gallery.PhotoFiles[" + index + "].FileName" });
            $(this).find("input[type=button]").attr({ "data-id": index, "id": "delphoto" + index });
            $(this).find("img").attr({ "id": "photoimg" + index });
        });
    }
}

//Delete info about preload image gellery before uploading
function ClearPhotoGallery() {
    var $elements = $(".galleryitem");
    $($elements).each(function (index) {
        if ($(this).attr("id") !== "galleryitem0") {

            $(this).remove();
        }
        else {
            ClearPhotoMediaData(0);
        }
    });
}

//Delete info about video link before uploading
function ClearVideoLink() {
    $("#videolink").val(null);
};

//Delete info about all preloaded media in form before uploading
function ClearAllMedia() {
    ClearPhotoMediaData(null);
    ClearPhotoGallery();
    ClearVideoLink();
};

function InitiateEditPhoto() {
    InitiateMediaPhoto();
}

function InitiateEditGallery() {
    InitiateMediaGallery();
}

function InitiateEditVideoLInk() {

};

function InitiateList() {
    $("#page-wrapper").on("click", ".edititem", function () {
        window.location.href = $(this).attr("data-url");
    });
    $("#page-wrapper").on("click", ".delitem", function () {
        if (confirm("Are you deleting " + $(this).attr("data-name") + "?")) {
            window.location.href = $(this).attr("data-url");
        }
    });
    $("#page-wrapper").on("click", ".activeitem", function () {
        window.location.href = $(this).attr("data-url");
    });
}

function InitiateFormWithContent() {
    InitValidation();
    //$("#page-wrapper").on("submit", ".formwithcontent", function (event) {
    //    event.preventDefault();
    //    var form = new FormData();
    //    $(".formwithcontent input").each(function () {
    //        var $input = $(this);
    //        if ($input.val() !== "" && $input.attr("type") !== "button" && $input.attr("type") !== "submit") {

    //            if ($input.attr("type") === "file") {
    //                var id = $(this).attr("id");
    //                var file = document.getElementById(id);
    //                form.append($input.attr("name"), file.files[0]);
    //            }
    //            else {
    //                form.append($input.attr("name"), $input.val());
    //            }

    //        }
    //    });
    //    var $textarea = $("textarea");
    //    if ($textarea !== null) {
    //        form.append($textarea.attr("name"), $textarea.val());
    //    }
    //    form.append($("select").attr("name"), $("select option:selected").val());
    //    $.ajax({
    //        url: $(this).attr("action"),
    //        type: "POST",
    //        success: function (data) {
    //            $("#page-wrapper").html(data);
    //        },
    //        data: form,
    //        cache: false,
    //        contentType: false,
    //        processData: false
    //    });
    //});
}