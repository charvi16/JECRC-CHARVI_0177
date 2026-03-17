import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentTrial } from './component-trial';

describe('ComponentTrial', () => {
  let component: ComponentTrial;
  let fixture: ComponentFixture<ComponentTrial>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComponentTrial],
    }).compileComponents();

    fixture = TestBed.createComponent(ComponentTrial);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
