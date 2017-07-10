
angular.module('permitirTab', []).directive('permitirTab', permitirTab);

function permitirTab() {
  return {
    require: 'ngModel',
    link: function (scope, elemento) {
      elemento.bind('keyup keydown', function (tecla) {
        let texto = this.value;
        if (tecla.keyCode === 9 && tecla.type === 'keydown') { 
          let inicio = this.selectionStart;
          let final = this.selectionEnd;

          this.value = texto.substring(0, inicio) + '\t' + texto.substring(final);          
          this.selectionStart = this.selectionEnd = inicio + 1;         
          tecla.preventDefault();            
        } 
      });
    }
  }
}