import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneMask'
})
export class PhoneMaskPipe implements PipeTransform {
  transform(numero: string): string {
      if (numero && (numero.length == 10 || numero.length == 11))
      {
        let ddd = numero.slice(0, 2);
        let resto = numero.slice(2);
        if (resto.length == 9)
        {
            return `(${ddd}) ${resto.slice(0,5)}-${resto.slice(5)}`;
        }
        return `(${ddd}) ${resto.slice(0,4)}-${resto.slice(4)}`;
      }
      return numero;
  }
}