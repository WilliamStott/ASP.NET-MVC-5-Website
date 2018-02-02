
//Anonymous jquery function (Protects functions inside)
$(function()
{
    //Global variable for tab and data type selected
    var bgType = "Default";
    var navTab = "Add";

    

    setListeners();

    //Sets all of the jquery listeners
    function setListeners()
    {
        //If the navbar restful example 3 is clicked promots the user for an id to delete then calls
        //the restfulDelete function with the id
        $("#restfulDelete").click(function () {
            var id = prompt("Enter an ID to delete using the restful API.", "1");
            //Ensures the id is an number
            restfulDelete(isNaN(id) ? 0 : id);
        });

        //Nav listeners
        $(".nav-tabs li").click(function () {
            navTab = $(this).text();
            //If the nav is on List load data from database
            //Shorthand if
            navTab == "List" && ajaxCalls(navTab);
            $(".nav-tabs li").removeClass("active");
            $(this).addClass("active");
            hideElements($(this).find("a").text());       
        });

        //Datatype button click listener
        $("#typeElement .btn").click(function ()
        {
            bgType = $(this).text();
            //If the nav is on List load data from database
            navTab == "List" && ajaxCalls(navTab);
            $("#typeElement .btn").removeClass("active")
            $(this).addClass("active");
             
        });

        //Setting jquery validate (A seperate jquery library for validation)
        //Validates the Add form to ensure that all inputs have been filled correctly
        $("#addElements").validate(
           {
               submitHandler: function (form) {
                   ajaxCalls("Add");
               }
           });

        //Checks when dropdown in list has been changed. Does an ajax call to retrieve the collected data.
        $("#listElements select").change(function () { ajaxCalls("List") });

        //Searches when user clicks the submit button on the search tab
        $("#searchElements button").click(function () { ajaxCalls("Search") });

    }

    //Hides and shows elements from selected tab
    function hideElements(currentTab)
    {
        $("#elements").children().hide();
        switch (currentTab)
        {
            case "Add":
                $("#addElements").show();
                break;
            case "List":
                $("#listElements, #typeElement").show();
                break;
            case "Search":
                $("#searchElements, #typeElement").show();
                break;
        }
    }

    //Handles all types of ajaxs calls
    function ajaxCalls(type)
    {
        switch(type)
        {

            case "Add":
                var duration, credits = 0;
                //Sets the variables using the data from creditElement and durationElement (To ensure both are integers)
                credits = $("#creditElement").val();
                duration = $("#durationElement").val();
               $.ajax({
                    url: "Home/addCourse", type: "POST", data: {
                        courseName: $("#nameElement").val(),
                        //Checks to make sure the submitted credit and duration is a number otherwise make it 0;
                        courseCredits: isNaN(credits) ? 0 : credits,
                        courseDuration: isNaN(duration) ? 0 : duration,
                        courseTutor: $("#tutorElement").val()
                    },
                    success: function (data)
                    {
                        //Alets telling the user if the request successfully got added to the database or not
                        data == "True" ? alert("Successfully Added Entry") : alert("Failed to add entry, please contact support for help")
                    },
                    error: function (data)
                    {
                        alert("There was an error communicating with the server. Please contact support for help.")
                    }
                });
                break;
            case "List":
                //Sets selectedOption to the dropdown option selected by the user
                var selectedOption = $('#listElements').find(":selected").val();
                $.ajax({
                    url: "Home/listCourse", type: "GET", data: {
                        returnType: bgType,
                        columnName: selectedOption
                    }, success: function (data) { displayData(data); }
                });
                break;
            case "Search":      
                var selectedOption = $('#searchElements').find(":selected").val();
                console.log(selectedOption);
                $.ajax({
                    url: "Home/searchCourse", type: "GET",
                    data: {
                        returnType: bgType,
                        columnName: selectedOption,
                        searchTerm: $("#searchBox").val()
                    }, success: function (data) { displayData(data); }
                });
                break;
        }
    }
    
    //Hides and displays the elements that show content shen tabs are changed
    function displayData(data)
    {
        $("#txtArea, #dataArea").hide();
        //Prints out the table if the type is set to default otherwise displays the data in a text box
        (bgType === "Default") ? $("#dataArea").show().empty().html(data) : $("#txtArea").show().text(data);       
    }

    function restfulDelete(id)
    {
        $.ajax({ url: "api/Restful/" + id, type: "DELETE", complete: function (data) { alert("Entry of id: " + id + " was deleted."); }});
    }
});

   
