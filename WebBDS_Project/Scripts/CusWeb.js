
$(function() {
    GetCity();
   
$('.lblcheckEmail').click(function () {
    CheckEmail();
 
});
});

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
           $('.dropDistrict').change(function () {
              
               var idDistric = $(".dropDistrict option:selected").val();
           
           });
       }
   });
}
function CheckEmail() {
    var valEmail = $('#TbBdsAdcount_Email').val();
    var url = "/Register/CheckEmail";


    $.ajax
   ({
       type: "POST",
       url: url,
       data: JSON.stringify({ Email: valEmail }),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
           $('#warrper').attr('value', data);
           if ($('#TbBdsAdcount_Email').val() == "") {

               toastr.error('Vui lòng nhập Email');
               t++;
           }
           else {
               if (data > 0) {
                   toastr.error('Email "' + $('#TbBdsAdcount_Email').val() + '" đã tồn tại');
               }
               else {
                   toastr.success('Email "' + $('#TbBdsAdcount_Email').val() + '" có thể sử dụng');
               }
           }
           
       }
   });

}