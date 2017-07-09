// Write your Javascript code.
(function ($) {
    $(document).ready(function () {

        $.ajax("/cart/info", {
            contentType: "application/json",
            method: "get"
        }).done(function (data) {
            $("span#cart-count").html(data.count);
            $("span#cart-sum").html(data.sum);
            $("nav p#cart-info").removeClass("hidden");
        });

        $(".cartAdd").click(function () {
            var productId = $(this).attr("data-productId");
            var uri = $(this).attr("data-addUri");
            $.ajax(uri, {
                contentType: "application/json",
                method: "post",
                data: JSON.stringify(productId)
            }).done(function (response) {
                if (response.success == true) {
                    $("span#cart-count").html(response.count);
                    $("span#cart-sum").html(response.sum);
                } else {
                    alert(response.message)
                }
            });
        });
    });
})(jQuery);