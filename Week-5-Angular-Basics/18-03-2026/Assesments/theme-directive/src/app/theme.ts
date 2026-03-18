import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
  selector: '[appTheme]',
  standalone: true
})
export class ThemeDirective implements OnChanges {

  @Input() appTheme: string = 'light';

  constructor(private el: ElementRef) {}

  ngOnChanges() {
    const element = this.el.nativeElement;

    if (this.appTheme === 'dark') {
      element.classList.add('dark-theme');
      element.classList.remove('light-theme');
    } else {
      element.classList.add('light-theme');
      element.classList.remove('dark-theme');
    }
  }
}