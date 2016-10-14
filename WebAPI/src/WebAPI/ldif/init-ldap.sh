#!/bin/bash

status () {
  echo "---> ${@}" >&2
}

set -x
: LDAP_ROOTPASS=${LDAP_ROOTPASS}
: LDAP_FQN_DOMAIN=${LDAP_FQN_DOMAIN}

LDAP_DOMAIN=ketan_ldap
export LDAP_DOMAIN

if [ ! -e /mnt/shared/ketan_ldap_bootstrapped ]; then
  status "Creation des elements pour le premier lancement"

  n=1
  until [ $n -ge 6 ]
  do
	echo "[${n}/5]Tentative de connexion au LDAP"
    ldapsearch -H ldap://${LDAP_DOMAIN}:389 -D cn=admin,$LDAP_FQN_DOMAIN -w $LDAP_ROOTPASS -x -d255 && break  # tentative de connexion au ldap
    n=$[$n+1]
	if [$n -e 6]; then
	  echo "Impossible de se connecter au LDAP sur le domaine ${LDAP_DOMAIN}"
	  exit -1
	fi
    sleep 5
  done
  
  echo "Initialisation des objets dans le LDAP"
  ldapadd -v -H ldap://${LDAP_DOMAIN}:389 -c -x -D cn=admin,$LDAP_FQN_DOMAIN -w $LDAP_ROOTPASS -f /mnt/ldif/user.ldif \
  && touch /mnt/shared/ketan_ldap_bootstrapped
else
  status "Elements deja crees"
fi
