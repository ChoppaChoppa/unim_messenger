package http

import "github.com/labstack/echo/v4"

type IHandlers interface {
	UserData(c echo.Context) error
}

type Server struct {
	*echo.Echo
	host     string
	handlers IHandlers
}

func New(host string, handlers IHandlers) *Server {
	return &Server{
		Echo:     echo.New(),
		host:     host,
		handlers: handlers,
	}
}

func (s *Server) Run() error {
	InitRouters(s)

	s.HideBanner = true
	if err := s.Echo.Start(s.host); err != nil {
		return err
	}

	return nil
}
