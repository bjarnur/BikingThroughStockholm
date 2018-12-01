const challenges = ` `

const lessons = ` `

const relatedWork = ` `

const technologies = ` `

new Vue({
  el: '#app',
  data: {
    users: [{
      name: "SONIA CAMACHO",
      email: "soniach@kth.se",
      pic: "sonia.jpg",
      contributions:[]
    }, {
      name: "JULIEN ROUAULT",
      email: "rouault@kth.se",
      pic: "julien.jpg",
      contributions:[]
    }, {
      name: "POORIA GHAVAMIAN",
      email: "pooriag@kth.se",
      pic: "pooria.jpg",
      contributions:[]
    }, {
      name: "BJARNI GUDMUNDSSON",
      email: "brgud@kth.se",
      pic: "bjarni.jpg",
      contributions:[]
    }, {
      name: "RAFA LUCENA",
      email: "rafaella@kth.se",
      pic: "rafa.jpg",
      contributions:[]
    }, {
      name: "HENRIQUE FURTADO",
      email: "hfm@kth.se",
      pic: "henrique.jpg",
      contributions:[]
    }],
    images: [{
      name: "1",
      description: " "
    }, {
      name: "2",
      description: " "
    }],
    challenges: challenges,
    lessons: lessons,
    relatedWork: relatedWork,
    technologies: technologies
  }
})

/////////////////////////////////////////////////////////////
// GALLERY
// Get the modal
var modal = document.getElementById('myModal');

// Get the image and insert it inside the modal - use its "alt" text as a caption
var modalImg = document.getElementById("img01");
var captionText = document.getElementById("caption");

function showImg(thisImg) {
    modal.style.display = "block";
    modalImg.src = thisImg.src;
    captionText.innerHTML = thisImg.alt;
}

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}
// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
  if (event.target == modal) {
      modal.style.display = "none";
  }
}

////////////////////////////////////////////////////////////////
// FOOTER
// Add a back to top animation to the button in the footer
$(document).ready(function () {

    // fade in #back-top
    $(function () {
        // scroll body to 0px on click
        $('#back-top a').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 800);
            return false;
        });
    });

});