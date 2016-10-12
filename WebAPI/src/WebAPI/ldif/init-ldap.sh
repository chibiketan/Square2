#!/bin/bash

status () {
  echo "---> ${@}" >&2
}

set -x
: LDAP_ROOTPASS=${LDAP_ROOTPASS}
: LDAP_FQN_DOMAIN=${LDAP_FQN_DOMAIN}

if [ ! -e /mnt/shared/ketan_ldap_bootstrapped ]; then
  status "Creation des elements pour le premier lancement"
  ping ketan.ldap
  ldapadd -v -h ketan.ldap:389 -c -x -D cn=admin,$LDAP_FQN_DOMAIN -w $LDAP_ROOTPASS -f /mnt/ldif/user.ldif \
  && touch /mnt/shared/ketan_ldap_bootstrapped
else
  status "Elements deja crees"
fi
