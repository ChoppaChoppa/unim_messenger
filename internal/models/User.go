package models

import (
	"github.com/gorilla/websocket"
)

type IAccount interface {
	Accept(visitor IVisitor) string
}

type IVisitor interface {
	VisitStudentAcc(acc Student) string
	VisitTeacherAcc(acc Teacher) string
}

type (
	NewUser struct {
		ID         int
		Connection *websocket.Conn
	}

	Student struct {
		Name  string
		Group int
	}

	Teacher struct {
		Name     string
		JobTitle string
	}
)

func (s Student) Accept(visitor IVisitor) string {
	return visitor.VisitStudentAcc(s)
}

func (t Teacher) Accept(visitor IVisitor) string {
	return visitor.VisitTeacherAcc(t)
}
