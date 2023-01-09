package http

import (
	"github.com/labstack/echo/v4"
)

type ILoginHandlers interface {
	Login(c echo.Context) error
}

type Server struct {
	*echo.Echo
	host  string
	login ILoginHandlers
}

func New(host string) *Server {
	return &Server{
		Echo: echo.New(),
		host: host,
	}
}

func (s *Server) WithLoginHandlers(handler ILoginHandlers) *Server {
	s.login = handler
	return s
}

func (s *Server) Run() error {
	InitRouters(s)

	s.HideBanner = true
	if err := s.Echo.Start(s.host); err != nil {
		return err
	}

	return nil
}
