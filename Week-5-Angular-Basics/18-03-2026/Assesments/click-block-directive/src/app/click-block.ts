import { Directive, ElementRef, Input, HostListener, OnChanges } from '@angular/core';

@Directive({
  selector: '[appClickBlock]',
  standalone: true
})
export class ClickBlockDirective implements OnChanges {

  @Input() appClickBlock: boolean = true;

  constructor(private el: ElementRef) {}

  ngOnChanges() {
    // Visual feedback
    if (!this.appClickBlock) {
      this.el.nativeElement.style.opacity = '0.5';
      this.el.nativeElement.style.cursor = 'not-allowed';
    } else {
      this.el.nativeElement.style.opacity = '1';
      this.el.nativeElement.style.cursor = 'pointer';
    }
  }

  @HostListener('click', ['$event'])
  onClick(event: Event) {
    if (!this.appClickBlock) {
      event.preventDefault();
      event.stopImmediatePropagation();
      alert('Action blocked 🚫');
    }
  }
}