package service

import "Messenger/internal/models"

func (s *service) Accept(visitor models.IVisitor, acc models.IAccount) string {
	return acc.Accept(visitor)
}
