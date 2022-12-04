package http

import (
	"github.com/labstack/echo/v4/middleware"
)

func InitRoutes(s *Server) {
	s.Use(middleware.Recover())
	s.Use(middleware.Logger())

	InitHealthHandler(s)
}

func InitHealthHandler(s *Server) {
	if s.healthHandler == nil {
		return
	}
	health := s.Group("/health")

	health.GET("/liveness", s.healthHandler.Liveness)
}
