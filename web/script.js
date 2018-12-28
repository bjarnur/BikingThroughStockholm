const motivation = `<p>
So, have you ever dreamt about biking on the streets of Stockholm Sweden? Right now you can, from the comfort of your local spinning hall! <i>Biking Through Stockholm</i> offers you the opportunity to experience a virtual bike ride through Stockholm, with the help of Virtual Reality technology. 
</p>
<p>
While you bike we keep track of your performance throughout the session. A part of the experience are challenges related to time spent biking, and training intensity, designed to help you get the most of your workout. See you on the streets of Stockholm!
</p>
<p style="font-size: 14pt;">
  This project was developed from September to early November 2018 as part of the DH2413 Advanced Graphics and Interaction course at KTH.
</p>`

const challenges = `<p>
At the very start of the project we were tasked with finding creative ways of using the <i>Backbone</i> camera. Many of the challenges we have faced during the course of the project work are in some ways related to the camera. This includes everything from finding meaningful ways to incorporate the camera in our project to technical issues. For example we had to figure out how to convert the video format from fisheye to spherical. We experimented with some tools for this, but ultimately realised that Unity can take care of this for us, if we import the video correctly into our Unity project. 
</p>
<p>
Other technical challenges include working with our motion sensor, <i>Kinect</i>, and our VR hardware <i>Oculus Rift</i>. None of us had ever worked with this specific hardware before, so it was a learning experience. However, after the initial learning curve our experience of working with both the <i>Kinect</i> and the <i>Oculus</i> is quite positive. 
</p> `

const lessons = `<p>
Many of the lessons learned relate in some way to the hardware we worked with, the <i>Oculus Rift</i>, <i>Kinect</i> and the <i>Backbone</i> camera. For instance, we need to be able to keep track of how our player moves their feet, while ignoring irrelevant noise going on in the background. This included both technical solutions, and being smart about where in a room to deploy the bike. 
</p>
<p>
Other challenges relate to other parts of the project. To implement interesting graphical effects we implemented our own shaders and particle systems. We also had to design the gamification elements of our application, for which we read up on gamification on a higher level. 
</p>
<p>
This is the second project this particular group worked on together, and at the start of the project we were already quite used to working together. This meant that, in general, the implementation went smoother and faster. A main difference in our way of work, compared to the previous project we worked on together, The King of the Cube, is that we worked in a more agile way this time, using week-long sprints. This turned out to be a great improvement, which made it easier for us to organize ourselves and prioritize tasks. 
</p> `

const relatedWork = `<p>
A big part of the inspiration behind <i>Biking Through Stockholm</i> project came from the project <a href="http://spinnulators.github.io/Spinnulator/"><i>Spinnulator</i></a>, which was developed by students in the Advanced Graphics and Interaction course in 2015. <i>Biking Through Stockholm</i> uses a similar hardware setup, with the addition of VR. Other differences between the two projects include the usage of real world footage, which is used in <i>BTS</i>, and an even stronger emphasis on the gamification of exercise, which we try to create with various gameplay mechanics. 
</p>
<p>
<a href="https://everysight.com/"><i>Everysight</i></a> is another project that gamifies exercise on bicycles. In this case exercise is gamified with the help of AR glasses, where information is superimposed on the real world which the biker sees through the glasses. This is a commercial project, aimed towards more elite cyclists.
</p> `

const technologies = `<p>
              The project is an augmented virtuality experience that uses pre-recorded video, to simulate a bike ride through Stockholm. The pre-recorded footage is on fisheye format, recorded with the <i>Backbone</i> camera, which can be viewed in VR with an <i>Oculus Rift</i>. Finally the project uses <i>Kinect</i> to detect players’ movement, which serves as the main interaction medium. 
              </p>
              <p>
              Since our project deals with physical activity, motivating the user is paramount. Therefore, we deployed “gameful design”, which strives to create an enjoyable interactive system. This has been done through purposeful gamification. To tie all of this together we used the <i>Unity</i> game engine. We had all worked with the engine before, and it made it easy for us to integrate all the different part of the project. 
              </p> `

new Vue({
  el: '#app',
  data: {
    users: [{
      name: "SONIA CAMACHO",
      email: "soniach@kth.se",
      linkedin: "https://www.linkedin.com/in/sonia-ch/",
      pic: "sonia.jpg",
      contributions:["Motion detection with Kinect","Footage recordings","GPS track adaptation","Website"]
    }, {
      name: "JULIEN ROUAULT",
      email: "rouault@kth.se",
      linkedin: "https://www.linkedin.com/in/julien-rouault/",
      pic: "julien.jpg",
      contributions:[]
    }, {
      name: "POORIA GHAVAMIAN",
      email: "pooriag@kth.se",
      linkedin: "https://www.linkedin.com/in/pooria-ghavamian/",
      pic: "pooria.jpg",
      contributions:[]
    }, {
      name: "BJARNI GUDMUNDSSON",
      email: "brgud@kth.se",
      linkedin: "https://www.linkedin.com/in/bjarni-ragnar-gudmundsson-17a12b100/",
      pic: "bjarni.jpg",
      contributions:[]
    }, {
      name: "RAFA LUCENA",
      email: "rafaella@kth.se",
      linkedin: "https://www.linkedin.com/in/rlaraujo/",
      pic: "rafa.jpg",
      contributions:[]
    }, {
      name: "HENRIQUE FURTADO",
      email: "hfm@kth.se",
      linkedin: "https://www.linkedin.com/in/henriquefur/",
      pic: "henrique.jpg",
      contributions:[]
    }],
    images: [{
      name: "BTS_1",
      description: "Demo at VIC studio"
    }, {
      name: "BTS_2",
      description: "Demo at VIC studio"
    }, {
      name: "BTS_3",
      description: "Open House at VIC studio"
    }, {
      name: "BTS_4",
      description: "Open House at VIC studio"
    }, {
      name: "BTS_5",
      description: "Open House at VIC studio"
    }, {
      name: "BTS_6",
      description: "Open House at VIC studio"
    }, {
      name: "BTS_7",
      description: "Open House at VIC studio"
    }, {
      name: "BTS_8",
      description: "Open House at VIC studio"
    }],
    motivation: motivation,
    challenges: challenges,
    lessons: lessons,
    relatedWork: relatedWork,
    technologies: technologies,
    tabs:[{
      id: "technologies",
      label: "Technologies"
    }, {
      id: "challenges",
      label: "Challenges"
    }, {
      id: "related-work",
      label: "Related Work"
    }, {
      id: "lessons",
      label: "Learning"
    }, {
      id: "gallery",
      label: "Gallery"
    }, {
      id: "team",
      label: "Team"
    }]
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

function scrollToElement(elementID) {
  $('body,html').animate({
    scrollTop: $(elementID).offset().top
}, 800);
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