
$(document).ready(function () {
    function redirectToHome() {
        // Redirecionamento para a página Home/Index
        window.location.href = '@Url.Action("Index", "Home")';
    }
})