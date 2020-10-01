(function (pagHome, $) {

    BloquearTela = function () {
        $.blockUI.defaults.border = 'none';
        $.blockUI({ message: "<div style='color: white'> <i class='fa fa-spinner fa-pulse fa-3x'></i> </div>", css: { border: '0px', backgroundColor: 'none' } });

        $(".blockOverlay").css("z-index", "2001");
        $(".blockUI").css("z-index", "2002");
    }

    DesbloquearTela = function () {
        $.unblockUI();
    }

    BloquearTela();

    if ($("#hidden_imagemequilizada").val() == "0") {
        $("#divoriginal").show();
    }

    $(function () {
        $('#upload').on('change', function () {
            var arquivo = this.files;

            if (arquivo && arquivo[0]) {

                if (!(/\.(gif|jpg|jpeg|tiff|png)$/i).test(arquivo[0].name)) {
                    alert('Arquivo de imagem invalido! Selecione um arquivo de imagem valida.');
                    return false;
                }

                $("#divoriginal").show();
                $("#divequilizada").hide();

                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imagem').attr('src', e.target.result);
                    $('#imagem').attr('alt', "Imagem Original");
                };

                reader.readAsDataURL(input.files[0]);

                $("#divbutton").show();
            }
        });

        $(document).on('click', '.btn_enviarimagem', function () {
            BloquearTela();

            return true;
        })
    });

    var input = document.getElementById('upload');
    var infoArea = document.getElementById('upload-label');

    input.addEventListener('change', ExibirDadosArquivo);

    function ExibirDadosArquivo(e) {
        var fileName = e.srcElement.files[0].name;
        infoArea.textContent = 'Nome arquivo: ' + fileName;
    }

    $(document).ready(function () {
        DesbloquearTela();
    });

})(window.pagHome = window.pagHome || {}, jQuery);

