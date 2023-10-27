$(document).foundation();

$.ajax('https://leonbicak35-001-site1.gtempurl.com/api/v1/kupac2',   // request url
    {
        success: function (data, status, xhr) {// success callback function
            //console.log(data);
            for(let i=0;i<data.length;i++){
                $('#podaci').append('<li>' + data[i].ime + '</li>');

            }
    }
});
