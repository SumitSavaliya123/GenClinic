import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors,
} from '@angular/forms';
import { Observable, of } from 'rxjs';

export function PasswordMatchValidator(controlName: string): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    const password = control.parent?.get(controlName)?.value;
    const repeatPassword = control.value;
    if (password === repeatPassword) {
      return of(null); // Passwords match
    } else {
      return of({ passwordMismatch: true }); // Passwords do not match
    }
  };
}
