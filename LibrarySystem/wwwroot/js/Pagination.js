(function ($) {
    $.fn.MyPagination = function (options) {
        var settings = $.extend({
            totalPages: 1,
            visiblePages: 5,
            onPageClick: function (pageNumber) { },
            currentPage: 1
        }, options);

        return this.each(function () {
            var $pagination = $(this);

            function renderPageButtons(currentPage) {
                var totalPages = settings.totalPages,
                    visiblePages = settings.visiblePages,
                    onPageClick = settings.onPageClick,
                    halfVisiblePages = Math.floor(visiblePages / 2),
                    startPage,
                    endPage,
                    i;

                $pagination.empty();

                if (totalPages <= visiblePages) {
                    startPage = 1;
                    endPage = totalPages;
                } else {
                    if (currentPage <= halfVisiblePages) {
                        startPage = 1;
                        endPage = visiblePages;
                    } else if (currentPage + halfVisiblePages >= totalPages) {
                        startPage = totalPages - visiblePages + 1;
                        endPage = totalPages;
                    } else {
                        startPage = currentPage - halfVisiblePages;
                        endPage = currentPage + halfVisiblePages;
                    }
                }

                var $ul = $('<ul class="pagination"></ul>').appendTo($pagination);

                var $firstBtn = $('<li class="page-item"><a class="page-link" href="#">First</a></li>')
                    .appendTo($ul)
                    .on('click', function () {
                        if (currentPage > 1) {
                            onPageClick(1);
                        }
                        return false;
                    });

                if (currentPage === 1) {
                    $firstBtn.addClass('disabled');
                }

                var $previousBtn = $('<li class="page-item"><a class="page-link" href="#">Previous</a></li>')
                    .appendTo($ul)
                    .on('click', function () {
                        if (currentPage > 1) {
                            onPageClick(currentPage - 1);
                        }
                        return false;
                    });

                if (currentPage === 1) {
                    $previousBtn.addClass('disabled');
                }

                for (i = startPage; i <= endPage; i++) {
                    var $pageBtn = $('<li class="page-item"><a class="page-link" href="#">' + i + '</a></li>')
                        .appendTo($ul)
                        .on('click', function () {
                            var pageNumber = parseInt($(this).text());
                            if (pageNumber !== currentPage) {
                                onPageClick(pageNumber);
                            }
                            return false;
                        });

                    if (i === currentPage) {
                        $pageBtn.addClass('active');
                    }
                }

                var $nextBtn = $('<li class="page-item"><a class="page-link" href="#">Next</a></li>')
                    .appendTo($ul)
                    .on('click', function () {
                        if (currentPage < totalPages) {
                            onPageClick(currentPage + 1);
                        }
                        return false;
                    });

                if (currentPage === totalPages) {
                    $nextBtn.addClass('disabled');
                }

                var $lastBtn = $('<li class="page-item"><a class="page-link" href="#">Last</a></li>')
                    .appendTo($ul)
                    .on('click', function () {
                        if (currentPage < totalPages) {
                            onPageClick(totalPages);
                        }
                        return false;
                    });

                if (currentPage === totalPages) {
                    $lastBtn.addClass('disabled');
                }
                if (totalPages  < 2) {
                    $pagination.empty();
                }

            }

            renderPageButtons(settings.currentPage);
        });
    };
})(jQuery);