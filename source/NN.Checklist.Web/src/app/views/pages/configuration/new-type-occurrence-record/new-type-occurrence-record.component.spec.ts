import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewTypeOccurrenceRecordComponent } from './new-type-occurrence-record.component';

describe('NewTypeOccurrenceRecordComponent', () => {
  let component: NewTypeOccurrenceRecordComponent;
  let fixture: ComponentFixture<NewTypeOccurrenceRecordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewTypeOccurrenceRecordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewTypeOccurrenceRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
