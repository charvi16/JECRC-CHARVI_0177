import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
  selector: '[appPriceHighlight]',
  standalone: true
})
export class PriceHighlightDirective implements OnChanges {

  @Input() appPriceHighlight!: number;

  constructor(private el: ElementRef) {}

  ngOnChanges() {
  const element = this.el.nativeElement;

  if (this.appPriceHighlight > 50000) {
    element.classList.add('high-price');
    element.classList.remove('low-price');
  } else {
    element.classList.add('low-price');
    element.classList.remove('high-price');
  }
}
}