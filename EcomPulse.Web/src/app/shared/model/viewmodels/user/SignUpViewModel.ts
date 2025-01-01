export class SignUpViewModel {
  username: string;
  password: string;
  email: string;
  phonenumber: string;
  address: string;
  city: string;
  county: string;
  constructor(
    username: string,
    password: string,
    email: string,
    phonenumber: string,
    address: string,
    city: string,
    county: string
  ) {
    this.username = username;
    this.password = password;
    this.email = email;
    this.phonenumber = phonenumber;
    this.address = address;
    this.city = city;
    this.county = county;
  }
}
