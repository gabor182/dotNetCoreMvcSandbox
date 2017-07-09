// Write your Javascript code.
(function ($) {
    $(document).ready(function () {
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