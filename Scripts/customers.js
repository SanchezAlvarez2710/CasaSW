$(document).ready(function () {
    $(".btn-close svg").click(function () {
        $(".modal-container").css("display", "none");
        document.location.href = "https://localhost:44369/CUSTOMERS/Index";
    });
});