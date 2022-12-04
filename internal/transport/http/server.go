package http

import "github.com/labstack/echo/v4"

type IHealthHandler interface {
	Liveness(c echo.Context) error
}

type Server struct {
	*echo.Echo
	host          string
	healthHandler IHealthHandler
}

func New(h string) *Server {
	return &Server{
		Echo: echo.New(),
		host: h,
	}
}

func (s *Server) Run() error {
	InitRoutes(s)
	s.HideBanner = true

	if err := s.Start(s.host); err != nil {
		return err
	}

	return nil
}

func (s *Server) WithHealthHandler(handler IHealthHandler) *Server {
	s.healthHandler = handler
	return s
}
