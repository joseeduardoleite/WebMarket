function addClasses() {
    $(".bg-light").each(function () {
        var scre = $("body").width();
        if (scre <= 768) {
            $("#btnGravar").addClass("btn-block");
        } else {
            $("#btnGravar").removeClass("btn-block");
        }

    });
}

function addClassesListagem(){
      $(".bg-light").each(function () {
        var scre = $("body").width();
        if (scre >= 768) {
            $("#listagem").addClass("p-5");
            $("#listagem").removeClass("p-3");
        } else {
            $("#listagem").removeClass("p-5");
            $("#listagem").addClass("p-3");
        }

    });  
}


$(document).ready(function () {
    // Adicionar classes ao carregar o documento
    addClasses();
    addClassesListagem();

    $(window).resize(function () {
        // Adicionar sempre que a tela for redimensionada
        addClasses();
        addClassesListagem();
    });
});