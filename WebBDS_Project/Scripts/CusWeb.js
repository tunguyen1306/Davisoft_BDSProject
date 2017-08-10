
$(function() {
    GetCity();
   // $('.dropDistrict').selectric();
   
$('.lblcheckEmail').click(function () {
    CheckEmail();
 
});
$(".datepicker1").datepicker({
    dateFormat: 'mm/dd/yy'
});
    
});
function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};
function GetCity() {
  
    //Get City
    var url = "/Register/GetCity";
    var stringCity = "<option selected=\"selected\" value=\"0\">Chọn thành phố</option>";

    $.ajax
   ({
       type: "POST",
       url: url,
       data: '',
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
            
           $.each(data, function (i, o) {
               stringCity += "<option value=" + o.CityId + ">" + o.CityName + "</option>";
           });
           $('#dropCity').html(stringCity);
           $('#dropCity').change(function () {
               var idCity = $("#dropCity option:selected").val();
               var idCityText = $("#dropCity option:selected").text();
               GetDistrict(idCity);

           });
          
          // $('#dropCity').selectric();
          
       }
   });
}
function GetDistrict(_id) {
   
    //Get City
    var url = "/Register/GetDistrict";
    var stringCity = "<option value=\"0\">Chọn quận huyện</option>";
    var obj = {};
    obj.id = _id;
    $('.dropDistrict').html('');
    $.ajax
   ({
       type: "POST",
       url: url,
       data: JSON.stringify(obj),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {

           $.each(data, function (i, o) {

               stringCity += "<option value=" + o.DistId + ">" + o.DistName + "</option>";

           });

           $('.dropDistrict').html(stringCity);
           //$('.dropDistrict').selectric();
           $('.dropDistrict').change(function () {
              
               var idDistric = $(".dropDistrict option:selected").val();
           
           });
         
       }
   });
}
function CheckEmail() {
    var valEmail = $('#TblBdsAdcount_Email').val();
    var url = "/Register/CheckEmail";


    $.ajax
   ({
       type: "POST",
       url: url,
       async: false,
       data: JSON.stringify({ Email: valEmail }),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
           $('#warrper').attr('value', data);
           if ($('#TblBdsAdcount_Email').val() == "") {

               toastr.error('Vui lòng nhập Email');
              
           }
           else
           {
               if (!isValidEmailAddress($('#TblBdsAdcount_Email').val())) {
                   toastr.error('Email không đúng định dạng');
               }
               else {
                   if (data > 0) {
                       toastr.error('Email "' + $('#TblBdsAdcount_Email').val() + '" đã tồn tại');
                   }
                   else {
                       toastr.success('Email "' + $('#TblBdsAdcount_Email').val() + '" có thể sử dụng');
                   }
               }
              
           }
           
       }
   });

}
