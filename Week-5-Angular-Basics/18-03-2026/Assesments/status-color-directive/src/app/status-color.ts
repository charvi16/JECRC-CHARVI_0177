import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
  selector: '[appStatusColor]',
  standalone: true
})
export class StatusColorDirective implements OnChanges {

  @Input() appStatusColor!: number;

  constructor(private el: ElementRef) {}

  ngOnChanges() {
    const element = this.el.nativeElement;

    if (this.appStatusColor >= 50) {
      element.style.color = '#2e7d32'; // green
      element.style.backgroundColor = '#e8f5e9';
      element.style.padding = '5px 10px';
      element.style.borderRadius = '6px';
    } else {
      element.style.color = '#c62828'; // red
      element.style.backgroundColor = '#ffebee';
      element.style.padding = '5px 10px';
      element.style.borderRadius = '6px';
    }
  }
}