package http

import (
	"github.com/labstack/echo/v4/middleware"
)

func InitRoutes(s *Server) {
	s.Use(middleware.Recover())
	s.Use(middleware.Logger())

	initHealthHandler(s)
	initWsHandler(s)
	initSerializeHandler(s)
	initPaymentHandler(s)
}

func initHealthHandler(s *Server) {
	if s.healthHandler == nil {
		return
	}
	health := s.Group("/health")

	health.GET("/liveness", s.healthHandler.Liveness)
}

func initWsHandler(s *Server) {
	if s.wsHandler == nil {
		return
	}

	s.GET("/ws", s.wsHandler.Upgrader)
}

func initSerializeHandler(s *Server) {
	serialize := s.Group("/serialize")
	serialize.GET("/json/student", s.serializeHandler.StudentJson)
	serialize.GET("/json/teacher", s.serializeHandler.TeacherJson)
	serialize.GET("/xml/student", s.serializeHandler.StudentXml)
	serialize.GET("/xml/teacher", s.serializeHandler.TeacherXml)
}

func initPaymentHandler(s *Server) {
	payment := s.Group("/pay")

	payment.POST("/some_pay", s.paymentHandler.SomePay)
}
