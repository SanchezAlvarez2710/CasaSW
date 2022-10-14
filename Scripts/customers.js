$(document).ready(function () {
    /*$(".bt-details").attr('data-toggle','.modelDetails');*/
    $(".bt-details").click(function(){
        //var x = $("#modalDetails");
        //x.load("~/CUSTOMERS/Details?id=" + 2, function () {
        //    x.dialog({
        //        show: {
        //            effect: "blind",
        //            duration: 1000
        //        },
        //        hide: {
        //            effect: "explode",
        //            duration: 1000
        //        },
        //        modal: true,
        //        resizable: false
        //    });
        //});
        //$(".modal-container").css("display", "flex");        
        alert('entro');
    });
    $(".btn-close svg").click(function () {
        $(".modal-container").css("display", "none");
    });
});