export class SignUpViewModel {
  userName: string;
  password: string;
  email: string;
  phoneNumber: string;
  address: string;
  city: string;
  county: string;
  constructor(
    userName: string,
    password: string,
    email: string,
    phoneNumber: string,
    address: string,
    city: string,
    county: string
  ) {
    this.userName = userName;
    this.password = password;
    this.email = email;
    this.phoneNumber = phoneNumber;
    this.address = address;
    this.city = city;
    this.county = county;
  }
}
