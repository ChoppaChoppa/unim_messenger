package models

import "fmt"

type Student struct {
	ID        string `json:"id"`
	FirstName string `json:"first_name"`
	LastName  string `json:"last_name"`
	Group     string `json:"group"`
	Age       int    `json:"age"`
	Course    int    `json:"course"`
	Msgs      []Msg  `json:"msgs"`
}

func (s *Student) String() string {
	return fmt.Sprintf("id: %s\nfirst name: %s\nlast name: %s\ngroupe: %s\nage: %v\ncourse: %v\n",
		s.ID,
		s.FirstName,
		s.LastName,
		s.Group,
		s.Age,
		s.Course)
}
