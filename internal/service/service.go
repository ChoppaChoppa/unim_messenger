package service

import (
	"Messenger/internal/models"
	"context"
	"fmt"
	"github.com/gorilla/websocket"
	"github.com/rs/zerolog"
	"sync"
)

var (
	userCount   int
	connections = make(map[int]*websocket.Conn)
)

type service struct {
	logger zerolog.Logger
	mutex  *sync.Mutex
}

func New(logger zerolog.Logger) *service {
	return &service{
		logger: logger,
		mutex:  &sync.Mutex{},
	}
}

func (s *service) NewConnection(ctx context.Context, conn *websocket.Conn) {
	s.mutex.Lock()
	connections[userCount] = conn
	userCount++
	s.mutex.Unlock()

	user := models.NewUser{
		Connection: conn,
		ID:         userCount,
	}

	go s.listen(user)
}

func (s *service) listen(user models.NewUser) {
	var msg models.Msg
	defer delete(connections, user.ID)
	defer user.Connection.Close()

	for {
		if err := user.Connection.ReadJSON(&msg); err != nil {
			s.logger.Err(err).Msg("failed to readJSON")
		}

		fmt.Println(msg.Text)
		go func() {
			err := writeMsg(msg)
			if err != nil {
				s.logger.Err(err).Msg("failed write")
			}
		}()
	}
}

func writeMsg(msg models.Msg) error {
	userTo, ok := connections[msg.ToID]
	if !ok {
		return fmt.Errorf("no conn")
	}

	if err := userTo.WriteJSON(msg); err != nil {
		return err
	}

	return nil
}
