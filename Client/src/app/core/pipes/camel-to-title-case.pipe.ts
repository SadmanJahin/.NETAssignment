import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'camelToTitleCase'
})
export class CamelToTitleCasePipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    if (!value) return '';

    const result = value.replace(/([a-z])([A-Z])/g, '$1 $2');

    return this.capitalizeFirstLetter(result);
  }

  private capitalizeFirstLetter(str: string): string {
    return str.charAt(0).toUpperCase() + str.slice(1);
  }
}
