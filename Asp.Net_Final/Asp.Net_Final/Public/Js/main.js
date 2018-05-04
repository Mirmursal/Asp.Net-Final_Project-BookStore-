$(function () {

 
    //Author filter begin
    $(".filterAuthor").click(function (e) {
        var selectedBooks;
        e.preventDefault();
        var authorID = $(this).attr("data-authorID");
        //Author ID is OKKAY
        $.ajax({
            url: "/AJAX/SortAuthor/",
            data: { authorID: authorID },
            datatype: "json",
            type: "post",
            success: function (res)
            {
                selectedBooks = res.data;
                $("ul.wrapBook").empty();
                if (selectedBooks.length == 0) {
                    var div = $("<div>").append("<h3 class='alert alert-info'>There is no book in this Author</h3>");
                    $("ul.wrapBook").append(div);
                }
                else {
                    for (var i = 0; i < selectedBooks.length; i++)
                    {
                        var li = $("<li class='col-md-3 bookWrapper list-group-item'></li>");
                        var imgWrap = $("<div class='imgWrapper'></div>");
                        var img = $("<img class='bookImg img-responsive' src='../Public/images/" + selectedBooks[i].Image + "'>");
                        imgWrap.append(img);
                        var divBookName = $("<div class='bookName'>" + selectedBooks[i].Name + "</div>");
                        var divDescription = $("<div class='bookDescription'>" + selectedBooks[i].Description + "</div>");

                        var divwrapButtons = $("<div class='wrap'></div>");
                        var btnGetIt = $("<a href='#' data-bookID="+selectedBooks[i].id+" class='button boughtBook'>Get It</a>");
                        var btndetails = $("<a href='#' data-bookID=" + selectedBooks[i].id +" class='button2 bookDetails' data-toggle='modal' data-target='#myModal'>Details</a>");

                        divwrapButtons.append(btndetails);
                        divwrapButtons.append(btnGetIt);

                        li.append(imgWrap);
                        li.append(divBookName);
                        li.append(divDescription);
                        li.append(divwrapButtons);

                        $("ul.wrapBook").append(li);
                    }
                }
                
            }
        })
    })
    //Author filter end
    //filter genre begin
    $(".filterGenre").click(function (e) {
        e.preventDefault();
        var selectedBooks;
        var genreID = $(this).attr("data-genreID");
        //genreId is OKKAY
        $.ajax({
            url: "/AJAX/SortGenre/",
            data: { genreID: genreID },
            datatype: "json",
            type: "post",
            success: function (res)
            {
                selectedBooks = res.data;
                $("ul.wrapBook").empty();
                if (selectedBooks.length == 0) {
                    var div = $("<div>").append("<h3 class='alert alert-info'>There is no book in this Genre</h3>");
                    $("ul.wrapBook").append(div);
                }
                else
                {
                    for (var i = 0; i < selectedBooks.length; i++) {
                        var li = $("<li class='col-md-3 bookWrapper list-group-item'></li>");
                        var imgWrap = $("<div class='imgWrapper'></div>");
                        var img = $("<img class='bookImg img-responsive' src='../Public/images/" + selectedBooks[i].Image + "'>");
                        imgWrap.append(img);
                        var divBookName = $("<div class='bookName'>" + selectedBooks[i].Name + "</div>");
                        var divDescription = $("<div class='bookDescription' >" + selectedBooks[i].Description + "</div>");

                        var divwrapButtons = $("<div class='wrap'></div>");
                        var btnGetIt = $("<a href='#' data-bookID=" + selectedBooks[i].id + "   class='button boughtBook'>Get It</a>");
                        btnGetIt.click()
                        var btndetails = $("<a href='#' data-bookID=" + selectedBooks[i].id + " class='button2 bookDetails' data-toggle='modal' data-target='#myModal'>Details</a>");

                        divwrapButtons.append(btndetails);
                        divwrapButtons.append(btnGetIt);

                        li.append(imgWrap);
                        li.append(divBookName);
                        li.append(divDescription);
                        li.append(divwrapButtons);

                        $("ul.wrapBook").append(li);
                    }
                }
            }
        })
    })
    //filter genre end
    //bought book begin
    $(document).on("click", ".boughtBook", function (e)
    {
        e.preventDefault();
        var bookID = $(this).attr("data-bookID");
        var btn = $(this);
        //bookID is OKKAY
        $.ajax({
            url: "/Ajax/CheckLogin",
            datatype: "json",
            type: "post",
            success: function (res) {
                if (res.status == 215) {
                    window.location = "/Login/Index";
                }
                else if (res.status == 200) {
                    $.ajax({
                        url: "/Ajax/CheckIsRead",
                        data: { bookID: bookID },
                        datatype: "json",
                        type: "post",
                        success: function (res) {
                            if (res.status == 215) {
                                var wrap = btn.parent().parent();
                                $(".bookMessage").css('display', 'none');
                                var div = $("<div class='alert alert-warning mt-2 bookMessage' style='position: absolute;bottom: 7 %;text - align: center;width: 88 %;padding: 9px;'>You have booked this book</div>");
                                wrap.append(div);
                                //bu kitabi artiq almisiniz
                            }
                            //llllllllllllllllllll
                            else if (res.status == 200) {
                                //var wrap = btn.parent().parent();
                                $(".bookMessage").css('display', 'none');
                                //var div = $("<div class='alert alert-info mt-2 bookMessage' style='position: absolute;bottom: 7 %;text - align: center;width: 88 %;padding: 9px;'>This book added your basket</div>");
                                //wrap.append(div);
                                //kitab elave edildi
                                $.ajax({
                                    url: "/Ajax/CheckLimit",
                                    datatype: "json",
                                    type: "post",
                                    success: function (res) {
                                        if (res.status == 215) {
                                            var wrap = btn.parent().parent();
                                            $(".bookMessage").css('display', 'none');
                                            var div = $("<div class='alert alert-warning mt-2 bookMessage' style='position: absolute;bottom: 7 %;text - align: center;width: 88 %;padding: 9px;'>Your basket is full</div>");
                                            wrap.append(div);
                                        }
                                        else if (res.status == 200) {
                                            $.ajax({
                                                url: "/Ajax/CheckBookCount",
                                                data: { bookID: bookID },
                                                datatype: "json",
                                                type: "post",
                                                success: function (res)
                                                {
                                                    if (res.status == 215)
                                                    {
                                                        var wrap = btn.parent().parent();
                                                        $(".bookMessage").css('display', 'none');
                                                        var div = $("<div class='alert alert-warning mt-2 bookMessage' style='position: absolute;bottom: 7 %;text - align: center;width: 88 %;padding: 9px;'>Unfortunately, this book has been exhausted.</div>");
                                                        wrap.append(div);
                                                    }
                                                    else if (res.status == 200)
                                                    {
                                                        $.ajax({
                                                            url: "/Ajax/AddBook",
                                                            data: { bookID: bookID },
                                                            datatype: "json",
                                                            type: "post",
                                                            success: function (res)
                                                            {
                                                                if (res.status == 200) {
                                                                    var wrap = btn.parent().parent();
                                                                    $(".bookMessage").css('display', 'none');
                                                                    var div = $("<div class='alert alert-warning mt-2 bookMessage' style='position: absolute;bottom: 7 %;text - align: center;width: 88 %;padding: 9px;'>You booked this book succesfully</div>");
                                                                    wrap.append(div);
                                                                }
                                                            }
                                                        })
                                                    }
                                                }
                                            })
                                        }
                                    }
                                })
                            }
                            //llllllllllllllllllllll
                        }
                    })
                }
            }
        })
    })
     //bought book end
    //book give back begin
    $(".btnBack").click(function (e) {
        e.preventDefault();
        $(this).css('color', 'white');

        var bookID = $(this).attr("data-bookId");
        console.log(bookID);
        //bookId is OKKAY
        $.ajax({
            url: "/AJAX/GiveBack/",
            data: { bookID: bookID },
            datatype: "json",
            type: "post",
            success: function (res) {
                if (res.status == 200)
                {
                    console.log("This book succesfully removed");
                    window.location.reload();
                }
            }
        })
    })
    //book give back end
    $(document).on('click', '.bookDetails', function (e) {
        e.preventDefault();
        var bookID = $(this).attr("data-bookID");
        //bookID is OKKAY
        $.ajax({
            url: "/AJAX/BookDetails/",
            data: { bookID: bookID },
            datatype: "json",
            type: "post",
            success: function (res) {
                if (res.status == 200) {
                    var selectedAuthor = res.data.AuthorList
                    var selectedBook = res.data.BookList;

                    //console.log("artiq modalini yaza bilersen");
                    console.log(selectedBook[0].Image);
                    //image here
                    $(".modal-image").attr("src", "Public/images/" + selectedBook[0].Image)
                   // image here end
                    $(".modalBookName").text(selectedBook[0].Name);
                    $(".modalDescription").text(selectedBook[0].Description);
                    var text = "";
                    for (var j = 0; j < selectedAuthor.length; j++)
                    {
                        text += selectedAuthor[j].Name + " " + selectedAuthor[j].Lastname +"\r\n";
                    }
                    $(".modalAuthorName").text(text);
                    $(".TotalCount").text("Total Count:" + selectedBook[0].TotalCount);
                    $(".BusyCount").text("Busy Count:" + selectedBook[0].BusyCount);
                }
            }
        })
    })
    //Keyup search begin
    $(".searchInput").keyup(function () {
        var inputText = (this).value;

        $.ajax({
            url: "/AJAX/Search/",
            data: { text: inputText },
            datatype: "json",
            type: "post",
            success: function (res) {
                selectedBooks = res.data;
                $("ul.wrapBook").empty();
                if (selectedBooks.length == 0) {
                    var div = $("<div>").append("<h3 class='alert alert-info'>There is no such book</h3>");
                    $("ul.wrapBook").append(div);
                }
                else {
                    for (var i = 0; i < selectedBooks.length; i++) {
                        var li = $("<li class='col-md-3 bookWrapper list-group-item'></li>");
                        var imgWrap = $("<div class='imgWrapper'></div>");
                        var img = $("<img class='bookImg img-responsive' src='../Public/images/" + selectedBooks[i].Image + "'>");
                        imgWrap.append(img);
                        var divBookName = $("<div class='bookName'>" + selectedBooks[i].Name + "</div>");
                        var divDescription = $("<div class='bookDescription' >" + selectedBooks[i].Description + "</div>");

                        var divwrapButtons = $("<div class='wrap'></div>");
                        var btnGetIt = $("<a href='#' data-bookID=" + selectedBooks[i].id + "   class='button boughtBook'>Get It</a>");
                        btnGetIt.click()
                        var btndetails = $("<a href='#' data-bookID=" + selectedBooks[i].id + " class='button2 bookDetails' data-toggle='modal' data-target='#myModal'>Details</a>");

                        divwrapButtons.append(btndetails);
                        divwrapButtons.append(btnGetIt);

                        li.append(imgWrap);
                        li.append(divBookName);
                        li.append(divDescription);
                        li.append(divwrapButtons);

                        $("ul.wrapBook").append(li);
                    }
                }
            }
        })
    });
    //Keyup searcj end

})