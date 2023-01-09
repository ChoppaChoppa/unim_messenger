package models

import "fmt"

var (
	TeacherRole Role
	StudentRole Role
)

type Role int

type User struct {
	Login    string
	Password string
	UserRole Role
}

func (u *User) String() string {
	return fmt.Sprintf("login: %s\npassword: %s\nrole: %v\n", u.Login, u.Password, u.UserRole)
}
