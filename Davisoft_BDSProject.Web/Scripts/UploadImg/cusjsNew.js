$(function () {


    $('.txtdiscount_products_tra').keyup(function () {

        if ($('.txtdiscount_products_tra').val() > 0) {
            var old = $('.txtoldprice_products_tra').val();
            console.log(old);
            var dis = $('.txtdiscount_products_tra').val();
            var total = (old * dis / 100)

            $('.txtnewprice_products_tra').val(old - total);
        }
    });
    if ($('.txtdiscount_products_tra').val() > 0) {
        $('.txtdiscount_products_tra').keydown(function () {
            var old = $('.txtoldprice_products_tra').val();
            var dis = $('.txtdiscount_products_tra').val();
            var total = (old * dis / 100)

            $('.txtnewprice_products_tra').val(old - total);
        });
    }

});
//Edit Upload
function Edit(ed) {
    $(ed).parents('.dz-preview').find('.cropper-container img').css({ "transform": "none" });
    $(ed).parents('.dz-preview').eq(0).find('.tools').removeClass('hidden');
    $(ed).parents('.dz-preview').eq(0).find('.final').addClass('hidden');
 
    $(ed).parents('.dz-preview').eq(0).find('.cropper-move').addClass('cropper-face');
    //hiden control drop
   // $(ed).parent().parent().toggleClass('cropper-hidden')
    $(ed).parent().parent().find('#imgmain img:first').addClass('cropper-hidden');
    $(ed).parent().parent().find('#imgmain img:first').next().removeClass('cropper-hidden');
    //hiden control drop end
    dropNew(ed);

}
function S4(ed) {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function GuidS4() {
    return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
}

//Delete Upload 
function DeleteUpload(del) {
   
    eventdelete(del);
    DeleteImg(del);
}

function eventdelete(del) {
    $(del).parents(".dz-preview").remove();
}


////Rotate
var val = 0;

function Rate(el) {
    val = val + 90;
    eventroate(el, val);
}

function eventroate(el, d) {

    //$(el).parents(".dz-preview").eq(0).find('.dz-image').eq(0).css({
    //    '-moz-transform': 'rotate(' + d + 'deg)',
    //    '-webkit-transform': 'rotate(' + d + 'deg)',
    //    '-o-transform': 'rotate(' + d + 'deg)',
    //    '-ms-transform': 'rotate(' + d + 'deg)',
    //    'transform': 'rotate(' + d + 'deg)'
    //});
    var isrotare0 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare0");
    var isrotare90=  $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare90");
    var isrotare180 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare180");
    var isrotare270 = $(el).parents(".dz-preview").eq(0).find('.dz-image').hasClass("rotare270");
    if (isrotare0)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare0").addClass('rotare90');
    if (isrotare90)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare90").addClass('rotare180');
    if (isrotare180)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare180").addClass('rotare270');
    if (isrotare270)
        $(el).parents(".dz-preview").eq(0).find('.dz-image').removeClass("rotare270").addClass('rotare0');
    
}
//Upload OK
function Ok(ed) {
    $(ed).parents('.dz-preview').eq(0).find('.tools').addClass('hidden');
    $(ed).parents('.dz-preview').eq(0).find('.final').removeClass('hidden');
    $(ed).parents('.dz-preview').eq(0).find('.cropper-move').removeClass('cropper-face');
   
}

//Zoom in

function ZoomIn(event) {
    ZoomIn(event);
}

function ZoomIn(el) {
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth() * 1.2);
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight() * 1.2);

};

//Zoom Out
function ZoomIOut(event) {
    ZoomOut(event);
}

function ZoomOut(el) {
    if (($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth() / 1.2) < 300) {

    } else {

        $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth() / 1.2);
        $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight() / 1.2);
    }
}

//Resize
function resizei(el) {
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerWidth(300));
    $(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight($(el).parents(".dz-preview").eq(0).find('#cusimg').eq(0).outerHeight(300));
}

//$('input[type="checkbox"].reset-data').checkbox({
//    checkedClass: 'icon-check',
//    uncheckedClass: 'icon-check-empty'
//});
