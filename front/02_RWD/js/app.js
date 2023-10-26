$(document).foundation();

$.ajax('https://localhost:7116/api/v1/kupac2',   // request url
    {
        success: function (data, status, xhr) {// success callback function
            //console.log(data);
            for(let i=0;i<data.length;i++){
                $('#podaci').append('<li>' + data[i].ime + '</li>');

            }
    }
});
