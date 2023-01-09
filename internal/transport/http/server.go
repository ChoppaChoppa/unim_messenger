package http

import "github.com/labstack/echo/v4"

type IHealthHandler interface {
	Liveness(c echo.Context) error
}

type IWsHandler interface {
	Upgrader(c echo.Context) error
}

type ISerializeHandler interface {
	StudentJson(c echo.Context) error
	TeacherJson(c echo.Context) error
	StudentXml(c echo.Context) error
	TeacherXml(c echo.Context) error
}

type IPaymentHandler interface {
	SomePay(c echo.Context) error
}

type Server struct {
	*echo.Echo
	host             string
	healthHandler    IHealthHandler
	wsHandler        IWsHandler
	serializeHandler ISerializeHandler
	paymentHandler   IPaymentHandler
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

func (s *Server) WithWsHandler(handler IWsHandler) *Server {
	s.wsHandler = handler
	return s
}

func (s *Server) WithSerializeHandler(handler ISerializeHandler) *Server {
	s.serializeHandler = handler
	return s
}

func (s *Server) WithPaymentHandler(handler IPaymentHandler) *Server {
	s.paymentHandler = handler
	return s
}
