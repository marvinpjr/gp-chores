(function($){
  $(function(){

    $('.sidenav').sidenav();

  }); // end of document ready
})(jQuery); // end of jQuery name space

var app = new Vue({
  el: '#app',
  data: {
    message: 'vue loaded'
  }
});
