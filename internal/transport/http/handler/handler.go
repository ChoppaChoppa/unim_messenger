package handler

import (
	"Messenger/internal/models"
	"context"
	"github.com/gorilla/websocket"
	"github.com/rs/zerolog"
)

type IService interface {
	NewConnection(ctx context.Context, conn *websocket.Conn)
	Accept(visitor models.IVisitor, acc models.IAccount) string
	SomePay(receiver models.Receiver) string
}

type Handler struct {
	logger  zerolog.Logger
	service IService
}

func New(logger zerolog.Logger, service IService) *Handler {
	return &Handler{
		logger:  logger,
		service: service,
	}
}
