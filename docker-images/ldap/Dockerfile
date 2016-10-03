FROM nickstenning/slapd
# RUN mkdir -p /docker-entrypoint-initdb.d
ADD ./ldif /mnt/ldif/

RUN ldapadd -v -h localhost:389 -c -x -D cn=admin,dc=funeco,dc=com -w $LDAP_ROOTPASS -f users.ldif
